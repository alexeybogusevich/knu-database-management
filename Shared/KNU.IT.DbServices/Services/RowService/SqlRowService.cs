using KNU.IT.DbManager.Connections;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.RowService
{
    public class SqlRowService : IRowService
    {
        private readonly AzureSqlDbContext context;

        public SqlRowService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<Row> GetRecordAsync(Guid id)
        {
            return await context.Rows.FirstOrDefaultAsync(r => r.Id.Equals(id));
        }

        public async Task<RowDTO> GetAsync(Guid id)
        {
            var dbRow = await GetRecordAsync(id);
            return new RowDTO { TableId = dbRow.TableId, Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(dbRow.Content) };
        }

        public async Task<List<RowDTO>> GetRowsAsync(Guid tableId)
        {
            return await context.Rows
                .Where(r => r.TableId.Equals(tableId))
                .Select(r => new RowDTO
                {
                    Id = r.Id,
                    TableId = r.TableId,
                    Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(r.Content)
                })
                .ToListAsync();
        }

        public async Task<Row> CreateAsync(Row row)
        {
            await context.Rows.AddAsync(row);
            await context.SaveChangesAsync();
            return row;
        }

        public async Task<Row> UpdateAsync(Row row)
        {
            context.Rows.Update(row);
            await context.SaveChangesAsync();
            return row;
        }

        public async Task DeleteAsync(Guid id)
        {
            var row = await GetRecordAsync(id);
            context.Rows.Remove(row);
            await context.SaveChangesAsync();
        }

        public async Task<List<RowDTO>> SearchByKeywordAsync(Guid tableId, string keyword, string column)
        {
            var rows = await context.Rows
                .Where(r => r.TableId.Equals(tableId))
                .ToListAsync();

            var comparer = StringComparison.OrdinalIgnoreCase;

            var tableSchema = (await context.Tables
                .FirstOrDefaultAsync(t => t.Id.Equals(tableId)))
                .Schema;

            var originalColumnName = JsonConvert.DeserializeObject<Dictionary<string, string>>(tableSchema)
                .FirstOrDefault(x => string.Equals(x.Key, column, comparer))
                .Key;

            return rows
                .AsEnumerable()
                .Select(r => new RowDTO
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
