using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
using KNU.IT.DBMSWebApi.Constants;
using KNU.IT.DbServices.Services.RowService;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KNU.IT.DBMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService tableService;
        private readonly IRowService rowService;

        public TableController(ITableService tableService, IRowService rowService)
        {
            this.tableService = tableService;
            this.rowService = rowService;
        }

        // GET: api/table
        [HttpGet("get/{id}", Name = RouteNames.TableGet)]
        public async Task<HATEOASResult> GetAsync(Guid id)
        {
            var table = await tableService.GetAsync(id);
            return this.HATEOASResult(table, (t) => Ok(t));
        }

        // GET: api/table
        [HttpGet("list/{databaseId}", Name = RouteNames.TableGetByDatabase)]
        public async Task<HATEOASResult> GetByDatabaseAsync(Guid databaseId)
        {
            var tables = await tableService.GetAllAsync(databaseId);
            var result = this.HATEOASResult(tables, (t) => Ok(t));
            return this.HATEOASResult(tables, (t) => Ok(t));
        }

        // GET: api/table
        [HttpGet("search/{tableId}", Name = RouteNames.TableSearch)]
        public async Task<HATEOASResult> SearchAsync(Guid tableId, string keyword, string column)
        {
            var rows = await rowService.SearchByKeywordAsync(tableId, keyword, column);
            return this.HATEOASResult(rows, (r) => Ok(r));
        }

        // POST: api/table
        [HttpPost("create", Name = RouteNames.TableCreate)]
        public async Task<HATEOASResult> CreateAsync([FromBody] Table table)
        {
            var createdTable = await tableService.CreateAsync(table);
            return this.HATEOASResult(createdTable, (t) => Ok(t));
        }

        // POST: api/table
        [HttpPost("update", Name = RouteNames.TableUpdate)]
        public async Task<HATEOASResult> UpdateAsync([FromBody] Table table)
        {
            var updatedTable = await tableService.UpdateAsync(table);
            return this.HATEOASResult(updatedTable, (t) => Ok(t));
        }

        // POST: api/table
        [HttpPost("delete/{id}", Name = RouteNames.TableDelete)]
        public async Task<HATEOASResult> GetRowsAsync(Guid id)
        {
            await tableService.DeleteAsync(id);
            return this.HATEOASResult(null, (t) => Ok(t));
        }
    }
}
