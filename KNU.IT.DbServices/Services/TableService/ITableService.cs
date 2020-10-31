using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.TableService
{
    public interface ITableService
    {
        Task<Table> GetAsync(Guid id);
        Task<TableViewModel> GetViewModelAsync(Guid id);
        Task<List<Table>> GetAllByDatabaseAsync(Guid databaseId);
        Task CreateAsync(Table table);
        Task UpdateAsync(Table table);
        Task DeleteAsync(Guid id);
    }
}