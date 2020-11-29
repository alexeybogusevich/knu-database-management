using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using KNU.IT.DbServices.Models.SettingModels;
using KNU.IT.DbServices.Services.TableService;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.RowService
{
    public class MongoRowService : IRowService
    {
        private readonly IMongoCollection<Row> rows;
        private readonly ITableService tableService;

        public MongoRowService(IMongoDatabaseSettings settings, ITableService tableService)
        {
            this.tableService = tableService;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            rows = database.GetCollection<Row>(settings.RowsCollectionName);
        }

        public async Task<Row> GetAsync(Guid id)
        {
            return await rows.Find(db => db.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Row>> GetAllAsync(Guid tableId)
        {
            return await rows.Find(r => r.TableId.Equals(tableId)).ToListAsync();
        }

        public async Task<List<Row>> SearchByKeywordAsync(Guid tableId, string keyword, string column)
        {
            var mongoRows = await rows.Find(r => r.TableId.Equals(tableId)).ToListAsync();

            var comparer = StringComparison.OrdinalIgnoreCase;

            var table = await tableService.GetAsync(tableId);

            var originalColumnName = JsonConvert.DeserializeObject<Dictionary<string, string>>(table?.Schema)
                .FirstOrDefault(x => String.Equals(x.Key, column, comparer))
                .Key;

            var rowDTOs = new List<RowResponse>();

            foreach (var row in mongoRows)
            {
                rowDTOs.Add(
                    new RowResponse
                    {
                        Id = row.Id,
                        TableId = row.TableId,
                        Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(row.Content)
                    });
            };

            var filteredRows = rowDTOs
                .Where(r => r.Content[originalColumnName].IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(r => r.Id)
                .ToList();

            return mongoRows.Where(r => filteredRows.Contains(r.Id)).ToList();
        }

        public async Task<Row> UpdateAsync(Row row)
        {
            await rows.ReplaceOneAsync(db => db.Id.Equals(row.Id), row);
            return row;
        }

        public async Task<Row> CreateAsync(Row row)
        {
            row.Id = Guid.NewGuid();
            await rows.InsertOneAsync(row);
            return row;
        }

        public async Task DeleteAsync(Guid id)
        {
            await rows.DeleteOneAsync(db => db.Id.Equals(id));
        }

        public async Task<bool> ValidateAsync(Row row)
        {
            var table = await tableService.GetAsync(row.TableId);

            var tableSchema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema);
            var rowColumns = JsonConvert.DeserializeObject<Dictionary<string, string>>(row.Content);

            foreach (var column in rowColumns)
            {
                var columnName = column.Key;
                var columnValue = column.Value;
                var columnType = Type.GetType(column.Value);

                var schemaType = tableSchema[column.Key].GetType();

                if (!schemaType.IsAssignableFrom(columnType))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
