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

        public async Task<Row> GetAsync(Guid id)
        {
            return await context.Rows.FirstOrDefaultAsync(r => r.Id.Equals(id));
        }

        public async Task<List<Row>> GetAllAsync(Guid tableId)
        {
            return await context.Rows
                .Where(r => r.TableId.Equals(tableId))
                .AsNoTracking()
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
            var row = await GetAsync(id);
            context.Rows.Remove(row);
            await context.SaveChangesAsync();
        }

        public async Task<List<Row>> SearchByKeywordAsync(Guid tableId, string keyword, string column)
        {
            var rows = await context.Rows
                .Include(r => r.Table)
                .Where(r => r.TableId.Equals(tableId))
                .AsNoTracking()
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
                .Select(r => new RowResponse
                {
                    Id = r.Id,
                    TableId = r.TableId,
                    Content = JsonConvert.DeserializeObject<Dictionary<string, string>>(r.Content)
                })
                .Where(r => r.Content[originalColumnName].IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(r => new Row
                {
                    Id = r.Id,
                    TableId = r.TableId,
                    Content = JsonConvert.SerializeObject(r.Content)
                })
                .ToList();
        }

        public async Task<bool> ValidateAsync(Row row)
        {
            var table = await context.Tables.FirstOrDefaultAsync(t => t.Id.Equals(row.TableId));

            var tableSchema = JsonConvert.DeserializeObject<Dictionary<string, string>>(table.Schema);
            var rowColumns = JsonConvert.DeserializeObject<Dictionary<string, string>>(row.Content);

            foreach(var column in rowColumns)
            {
                var columnName = column.Key;
                var columnValue = column.Value;

                var schemaType = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .FirstOrDefault(t => t.Name.Equals(tableSchema[columnName]));

                try
                {
                    var unboxedObject = Convert.ChangeType(columnValue, schemaType);
                }
                catch(InvalidCastException ex)
                {
                    return false;
                }
                catch(FormatException ex)
                {
                    return false;
                }
                catch(OverflowException ex)
                {
                    return false;
                }
                catch(ArgumentNullException ex)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
