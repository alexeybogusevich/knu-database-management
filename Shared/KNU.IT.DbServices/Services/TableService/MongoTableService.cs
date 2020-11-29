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

        public MongoTableService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            tables = database.GetCollection<Table>(settings.TablesCollectionName);
        }

        public async Task<List<Table>> GetAllAsync(Guid databaseId)
        {
            return await tables
                .Find(t => t.DatabaseId.Equals(databaseId))
                .ToListAsync();
        }

        public async Task<Table> GetAsync(Guid id)
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
