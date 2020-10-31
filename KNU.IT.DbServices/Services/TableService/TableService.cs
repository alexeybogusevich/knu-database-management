using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbServices.Models;

namespace KNU.IT.DbServices.Services.TableService
{
    public class TableService : ITableService
    {
        private readonly AzureSqlDbContext context;

        public TableService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<Table> GetRecordAsync(Guid id)
        {
            return await context.Tables.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<TableDTO> GetAsync(Guid id)
        {
            var table = await context.Tables.FirstOrDefaultAsync(t => t.Id.Equals(id));
            return new TableDTO
            {
                Id = table.Id,
                Name = table.Name,
                DatabaseId = table.DatabaseId,
                Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema)
            };
        }

        public async Task<List<TableDTO>> GetAllAsync(Guid databaseId)
        {
            return await context.Tables
                .Where(t => t.DatabaseId.Equals(databaseId))
                .AsNoTracking()
                .Select(table => new TableDTO
                {
                    Id = table.Id,
                    Name = table.Name,
                    DatabaseId = table.DatabaseId,
                    Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema)
                })
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
            var table = await GetRecordAsync(id);
            context.Tables.Remove(table);
            await context.SaveChangesAsync();
        }
    }
}
