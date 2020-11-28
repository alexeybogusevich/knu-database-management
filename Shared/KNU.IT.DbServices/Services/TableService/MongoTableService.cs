using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using KNU.IT.DbServices.Models.SettingModels;
using KNU.IT.DbServices.Services.DatabaseService;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.TableService
{
    public class MongoTableService : ITableService
    {
        private readonly IMongoCollection<Table> tables;
        private readonly IDatabaseService databaseService;

        public MongoTableService(IMongoDatabaseSettings settings, IDatabaseService databaseService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            tables = database.GetCollection<Table>(settings.TablesCollectionName);
            this.databaseService = databaseService;
        }

        public async Task<List<TableResponse>> GetAllAsync(Guid databaseId)
        {
            var result = new List<TableResponse>();

            var mongoTables = await tables
                .Find(t => t.DatabaseId.Equals(databaseId))
                .ToListAsync();

            var database = await databaseService.GetAsync(databaseId);

            foreach (var table in mongoTables)
            {
                result.Add(
                    new TableResponse
                    {
                        Id = table.Id,
                        Name = table.Name,
                        DatabaseId = table.DatabaseId,
                        DatabaseName = database?.Name,
                        Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema)
                    });
            }

            return result;
        }

        public async Task<TableResponse> GetAsync(Guid id)
        {
            var table = await tables.Find(db => db.Id == id).FirstOrDefaultAsync();
            var database = await databaseService.GetAsync(table.DatabaseId);

            return new TableResponse
            {
                Id = table.Id,
                Name = table.Name,
                DatabaseId = table.DatabaseId,
                DatabaseName = database?.Name,
                Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema)
            };
        }

        public async Task<Table> GetRecordAsync(Guid id)
        {
            return await tables.Find(db => db.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Table> UpdateAsync(Table table)
        {
            await tables.ReplaceOneAsync(db => db.Id.Equals(table.Id), table);
            return table;
        }

        public async Task<Table> CreateAsync(Table table)
        {
            table.Id = Guid.NewGuid();
            await tables.InsertOneAsync(table);
            return table;
        }

        public async Task DeleteAsync(Guid id)
        {
            await tables.DeleteOneAsync(db => db.Id.Equals(id));
        }
    }
}
