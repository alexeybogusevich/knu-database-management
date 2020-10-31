using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Services.RowService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KNU.IT.DBMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowController : ControllerBase
    {
        private readonly IRowService rowService;

        public RowController(IRowService rowService)
        {
            this.rowService = rowService;
        }

        // GET: api/row
        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var rows = await rowService.GetAsync(id);
            return Ok(rows);
        }

        // GET: api/row
        [HttpGet("list/{tableId}")]
        public async Task<ActionResult> GetByTableAsync(Guid tableId)
        {
            var rows = await rowService.GetRowsAsync(tableId);
            return Ok(rows);
        }

        // POST: api/row
        [HttpPost("update")]
        public async Task<ActionResult> UpdateAsync([FromBody] Row row)
        {
            var updatedRow = await rowService.UpdateAsync(row);
            return Ok(updatedRow);
        }

        // POST: api/row
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync([FromBody] Row row)
        {
            var createdRow = await rowService.CreateAsync(row);
            return Ok(createdRow);
        }

        // POST: api/row
        [HttpPost("delete/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await rowService.DeleteAsync(id);
            return Ok();
        }
    }
}
