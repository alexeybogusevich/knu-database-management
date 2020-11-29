using KNU.IT.DbManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.TableService
{
    public interface ITableService
    {
        Task<Table> GetAsync(Guid id);
        Task<List<Table>> GetAllAsync(Guid databaseId);
        Task<Table> CreateAsync(Table table);
        Task<Table> UpdateAsync(Table table);
        Task DeleteAsync(Guid id);
    }
}