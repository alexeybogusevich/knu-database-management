using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using KNU.IT.DbServices.Models.SettingModels;
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

        public MongoTableService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            tables = database.GetCollection<Table>(settings.TablesCollectionName);
        }

        public async Task<List<TableDTO>> GetAllAsync(Guid databaseId)
        {
            var result = new List<TableDTO>();

            var mongoTables = await tables
                .Find(t => t.DatabaseId.Equals(databaseId))
                .ToListAsync();

            foreach(var table in mongoTables)
            {
                result.Add(
                    new TableDTO
                    {
                        Id = table.Id,
                        Name = table.Name,
                        DatabaseId = table.DatabaseId,
                        Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema)
                    });
            }

            return result;
        }

        public async Task<TableDTO> GetAsync(Guid id)
        {
            var table = await tables.Find(db => db.Id == id).FirstOrDefaultAsync();
            return new TableDTO
            {
                Id = table.Id,
                Name = table.Name,
                DatabaseId = table.DatabaseId,
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
