using KNU.IT.DbManagementSystem.Models;
using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public async Task<RowViewModel> GetViewModelAsync(Guid id)
        {
            var dbRow = await GetAsync(id);
            return new RowViewModel { TableId = dbRow.TableId, Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(dbRow.Content) };
        }

        public async Task<List<RowViewModel>> GetAllViewModelsByTableAsync(Guid tableId)
        {
            return await context.Rows
                .Where(r => r.TableId.Equals(tableId))
                .Select(r => new RowViewModel
                {
                    Id = r.Id,
                    TableId = r.TableId,
                    Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(r.Content)
                })
                .ToListAsync();
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

        public async Task<List<RowViewModel>> SearchByKeywordAsync(Guid tableId, string keyword, string column)
        {
            var rows = await context.Rows
                .Where(r => r.TableId.Equals(tableId))
                .ToListAsync();

            var comparer = StringComparison.OrdinalIgnoreCase;

            var tableSchema = (await context.Tables
                .FirstOrDefaultAsync(t => t.Id.Equals(tableId)))
                .Schema;

            var originalColumnName = JsonConvert.DeserializeObject<Dictionary<string, string>>(tableSchema)
                .FirstOrDefault(x => String.Equals(x.Key, column, comparer))
                .Key;

            return rows
                .AsEnumerable()
                .Select(r => new RowViewModel
                {
                    Id = r.Id,
                    TableId = r.TableId,
                    Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(r.Content)
                })
                .Where(r => r.Content[originalColumnName].IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }
    }
}
