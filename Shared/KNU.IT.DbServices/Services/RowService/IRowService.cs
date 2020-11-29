using KNU.IT.DbManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.RowService
{
    public interface IRowService
    {
        Task<Row> GetAsync(Guid id);
        Task<Row> CreateAsync(Row row);
        Task<Row> UpdateAsync(Row row);
        Task DeleteAsync(Guid id);
        Task<List<Row>> GetAllAsync(Guid tableId);
        Task<List<Row>> SearchByKeywordAsync(Guid tableId, string keyword, string column);
    }
}