using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.RowService
{
    public interface IRowService
    {
        Task<Row> GetAsync(Guid id);
        Task CreateAsync(Row row);
        Task UpdateAsync(Row row);
        Task DeleteAsync(Guid id);
        Task<List<RowViewModel>> GetAllViewModelsByTableAsync(Guid tableId);
        Task<List<RowViewModel>> SearchByKeywordAsync(Guid tableId, string keyword, string column);
    }
}