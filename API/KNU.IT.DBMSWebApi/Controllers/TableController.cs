using KNU.IT.DbManager.Models;
using KNU.IT.DBMSWebApi.Constants;
using KNU.IT.DbServices.Converters;
using KNU.IT.DbServices.Services.RowService;
using KNU.IT.DbServices.Services.TableService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet("{id}", Name = RouteNames.TableGet)]
        [ProducesResponseType(typeof(Table), 200)]
        public async Task<HATEOASResult> GetAsync(Guid id)
        {
            var table = await tableService.GetAsync(id);
            var tableResponse = TableConverter.GetTableResponse(table);
            return this.HATEOASResult(tableResponse, (t) => Ok(t));
        }

        // GET: api/table
        [HttpGet("list/{databaseId}", Name = RouteNames.TableGetByDatabase)]
        [ProducesResponseType(typeof(Table), 200)]
        public async Task<HATEOASResult> GetByDatabaseAsync(Guid databaseId)
        {
            var tables = await tableService.GetAllAsync(databaseId);
            var tablesResponse = tables.Select(t => TableConverter.GetTableResponse(t)).ToList();
            return this.HATEOASResult(tablesResponse, (t) => Ok(t));
        }

        // GET: api/table
        [HttpGet("search/{tableId}", Name = RouteNames.TableSearch)]
        [ProducesResponseType(typeof(Row), 200)]
        public async Task<HATEOASResult> SearchAsync(Guid tableId, string keyword, string column)
        {
            var rows = await rowService.SearchByKeywordAsync(tableId, keyword, column);
            var rowsResponse = rows.Select(r => RowConverter.GetRowResponse(r)).ToList();
            return this.HATEOASResult(rowsResponse, (r) => Ok(r));
        }

        // POST: api/table
        [HttpPost(Name = RouteNames.TableCreate)]
        [ProducesResponseType(200)]
        public async Task<HATEOASResult> CreateAsync([FromBody] Table table)
        {
            var createdTable = await tableService.CreateAsync(table);
            var tableResponse = TableConverter.GetTableResponse(createdTable);
            return this.HATEOASResult(tableResponse, (t) => Ok(t));
        }

        // POST: api/table
        [HttpPut(Name = RouteNames.TableUpdate)]
        [ProducesResponseType(200)]
        public async Task<HATEOASResult> UpdateAsync([FromBody] Table table)
        {
            var updatedTable = await tableService.UpdateAsync(table);
            var tableResponse = TableConverter.GetTableResponse(updatedTable);
            return this.HATEOASResult(updatedTable, (t) => Ok(t));
        }

        // POST: api/table
        [HttpDelete("{id}", Name = RouteNames.TableDelete)]
        [ProducesResponseType(200)]
        public async Task<HATEOASResult> GetRowsAsync(Guid id)
        {
            await tableService.DeleteAsync(id);
            return this.HATEOASResult(null, (t) => Ok(t));
        }
    }
}
