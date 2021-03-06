﻿using KNU.IT.DbManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KNU.IT.DbServices.Services.DatabaseService
{
    public interface IDatabaseService
    {
        Task<Database> GetAsync(Guid id);
        Task<List<Database>> GetAllAsync();
        Task<Database> CreateAsync(Database database);
        Task<Database> UpdateAsync(Database database);
        Task DeleteAsync(Guid id);
    }
}