using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.RowService
{
    public interface IRowService
    {
        Task<RowDTO> GetAsync(Guid id);
        Task<Row> GetRecordAsync(Guid id);
        Task<Row> CreateAsync(Row row);
        Task<Row> UpdateAsync(Row row);
        Task DeleteAsync(Guid id);
        Task<List<RowDTO>> GetRowsAsync(Guid tableId);
        Task<List<RowDTO>> SearchByKeywordAsync(Guid tableId, string keyword, string column);
    }
}