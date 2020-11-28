using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.DatabaseService
{
    public class SqlDatabaseService : IDatabaseService
    {
        private readonly AzureSqlDbContext context;

        public SqlDatabaseService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<Database> GetAsync(Guid id)
        {
            return await context.Databases.FirstOrDefaultAsync(d => d.Id.Equals(id));
        }

        public async Task<List<Database>> GetAllAsync()
        {
            return await context.Databases.ToListAsync();
        }

        public async Task<Database> CreateAsync(Database database)
        {
            await context.Databases.AddAsync(database);
            await context.SaveChangesAsync();
            return database;
        }

        public async Task<Database> UpdateAsync(Database database)
        {
            context.Databases.Update(database);
            await context.SaveChangesAsync();
            return database;
        }

        public async Task DeleteAsync(Guid id)
        {
            var database = await GetAsync(id);
            context.Databases.Remove(database);
            await context.SaveChangesAsync();
        }
    }
}
