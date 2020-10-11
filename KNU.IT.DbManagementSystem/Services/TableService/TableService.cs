using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.DbManagementSystem.Services.TableService
{
    public class TableService : ITableService
    {
        private readonly AzureSqlDbContext context;

        public TableService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<Table> GetAsync(Guid id)
        {
            return await context.Tables.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<List<Table>> GetAllByDatabaseAsync(Guid databaseId)
        {
            return await context.Tables.Where(t => t.DatabaseId.Equals(databaseId)).ToListAsync();
        }

        public async Task CreateAsync(Table table)
        {
            await context.Tables.AddAsync(table);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Table table)
        {
            context.Tables.Update(table);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var table = await GetAsync(id);
            context.Tables.Remove(table);
            await context.SaveChangesAsync();
        }
    }
}
