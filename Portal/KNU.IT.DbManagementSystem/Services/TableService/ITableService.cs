using KNU.IT.DbManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbManagementSystem.Services.TableService
{
    public interface ITableService
    {
        Task<Table> GetAsync(Guid id);
        Task<List<Table>> GetAllByDatabaseAsync(Guid databaseId);
        Task CreateAsync(Table table);
        Task UpdateAsync(Table table);
        Task DeleteAsync(Guid id);
    }
}