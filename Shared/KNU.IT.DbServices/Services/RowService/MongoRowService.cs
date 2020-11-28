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

        public async Task<RowResponse> GetAsync(Guid id)
        {
            var row = await rows.Find(db => db.Id == id).FirstOrDefaultAsync();
            var table = await tableService.GetAsync(row.TableId);
            return new RowResponse
            {
                Id = row.Id,
                TableId = row.TableId,
                TableName = table?.Name,
                Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(row.Content)
            };
        }

        public async Task<Row> GetRecordAsync(Guid id)
        {
            return await rows.Find(db => db.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<RowResponse>> GetRowsAsync(Guid tableId)
        {
            var result = new List<RowResponse>();
            var mongoRows = await rows.Find(r => r.TableId.Equals(tableId)).ToListAsync();
            var table = await tableService.GetAsync(tableId);
            foreach (var row in mongoRows)
            {
                result.Add(
                    new RowResponse
                    {
                        Id = row.Id,
                        TableId = row.TableId,
                        TableName = table?.Name,
                        Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(row.Content)
                    });
            }

            return result;
        }

        public async Task<List<RowResponse>> SearchByKeywordAsync(Guid tableId, string keyword, string column)
        {
            var mongoRows = await rows.Find(r => r.TableId.Equals(tableId)).ToListAsync();

            var comparer = StringComparison.OrdinalIgnoreCase;

            var table = await tableService.GetRecordAsync(tableId);

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
                        TableName = table?.Name,
                        Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(row.Content)
                    });
            };

            return rowDTOs.Where(r => r.Content[originalColumnName].IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
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
    }
}
