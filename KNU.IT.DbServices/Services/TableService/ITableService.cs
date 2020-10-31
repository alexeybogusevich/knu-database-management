using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.TableService
{
    public interface ITableService
    {
        Task<Table> GetRecordAsync(Guid id);
        Task<TableDTO> GetAsync(Guid id);
        Task<List<Table>> GetAllAsync(Guid databaseId);
        Task<Table> CreateAsync(Table table);
        Task<Table> UpdateAsync(Table table);
        Task DeleteAsync(Guid id);
    }
}