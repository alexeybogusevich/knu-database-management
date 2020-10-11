using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.DbManagementSystem.Services.RowService
{
    public class RowService : IRowService
    {
        private readonly AzureSqlDbContext context;

        public RowService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<Row> GetAsync(Guid id)
        {
            return await context.Rows.FirstOrDefaultAsync(r => r.Id.Equals(id));
        }

        public async Task<List<Row>> GetAllByTableAsync(Guid tableId)
        {
            return await context.Rows.Where(r => r.TableId.Equals(tableId)).ToListAsync();
        }

        public async Task CreateAsync(Row row)
        {
            await context.Rows.AddAsync(row);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Row row)
        {
            context.Rows.Update(row);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var row = await GetAsync(id);
            context.Rows.Remove(row);
            await context.SaveChangesAsync();
        }
    }
}
