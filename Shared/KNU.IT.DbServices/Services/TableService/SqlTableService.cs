using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public async Task<Table> GetRecordAsync(Guid id)
        {
            return await context.Tables.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<TableResponse> GetAsync(Guid id)
        {
            var table = await context.Tables.FirstOrDefaultAsync(t => t.Id.Equals(id));
            return new TableResponse
            {
                Id = table.Id,
                Name = table.Name,
                DatabaseId = table.DatabaseId,
                DatabaseName = table.Database.Name,
                Schema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema)
            };
        }

        public async Task<List<TableResponse>> GetAllAsync(Guid databaseId)
        {
            return await context.Tables
                .Where(t => t.DatabaseId.Equals(databaseId))
                .AsNoTracking()
                .Select(table => new TableResponse
                {
                    Id = table.Id,
                    Name = table.Name,
                    DatabaseId = table.DatabaseId,
                    DatabaseName = table.Database.Name,
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
