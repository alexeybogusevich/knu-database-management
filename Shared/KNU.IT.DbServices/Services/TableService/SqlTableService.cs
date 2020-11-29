using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.TableService
{
    public class SqlTableService : ITableService
    {
        private readonly AzureSqlDbContext context;

        public SqlTableService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<Table> GetAsync(Guid id)
        {
            return await context.Tables.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<List<Table>> GetAllAsync(Guid databaseId)
        {
            return await context.Tables
                .Where(t => t.DatabaseId.Equals(databaseId))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Table> CreateAsync(Table table)
        {
            await context.Tables.AddAsync(table);
            await context.SaveChangesAsync();
            return table;
        }

        public async Task<Table> UpdateAsync(Table table)
        {
            context.Tables.Update(table);
            await context.SaveChangesAsync();
            return table;
        }

        public async Task DeleteAsync(Guid id)
        {
            var table = await GetAsync(id);
            context.Tables.Remove(table);
            await context.SaveChangesAsync();
        }
    }
}
