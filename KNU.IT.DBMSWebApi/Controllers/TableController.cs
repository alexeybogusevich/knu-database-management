using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
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

        public TableController(ITableService tableService)
        {
            this.tableService = tableService;
        }

        // GET: api/table
        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var table = await tableService.GetAsync(id);
            return Ok(table);
        }

        // GET: api/table
        [HttpGet("list/{databaseId}")]
        public async Task<ActionResult> GetByDatabaseAsync(Guid databaseId)
        {
            var tables = await tableService.GetAllAsync(databaseId);
            return Ok(tables);
        }

        // POST: api/table
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync([FromBody] Table table)
        {
            var createdTable = await tableService.CreateAsync(table);
            return Ok(createdTable);
        }

        // POST: api/table
        [HttpPost("update")]
        public async Task<ActionResult> UpdateAsync([FromBody] Table table)
        {
            var updatedTable = await tableService.UpdateAsync(table);
            return Ok(updatedTable);
        }

        // POST: api/table
        [HttpPost("delete/{id}")]
        public async Task<ActionResult> GetRowsAsync(Guid id)
        {
            await tableService.DeleteAsync(id);
            return Ok();
        }
    }
}
