using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models.SettingModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.DatabaseService
{
    public class MongoDatabaseService : IDatabaseService
    {
        private readonly IMongoCollection<Database> databases;

        public MongoDatabaseService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            databases = database.GetCollection<Database>(settings.DatabasesCollectionName);
        }

        public async Task<List<Database>> GetAllAsync()
        {
            return await databases.Find(db => true).ToListAsync();
        }

        public async Task<Database> GetAsync(Guid id)
        {
            return await databases.Find(db => db.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Database> CreateAsync(Database database)
        {
            database.Id = Guid.NewGuid();
            await databases.InsertOneAsync(database);
            return database;
        }

        public async Task<Database> UpdateAsync(Database database)
        {
            await databases.ReplaceOneAsync(db => db.Id.Equals(database.Id), database);
            return database;
        }

        public async Task DeleteAsync(Guid id)
        {
            await databases.DeleteOneAsync(db => db.Id.Equals(id));
        }
    }
}
