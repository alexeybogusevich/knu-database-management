using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
using KNU.IT.DBMSWebApi.Constants;
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
        [HttpGet("get/{id}", Name = RouteNames.RowGet)]
        public async Task<HATEOASResult> GetAsync(Guid id)
        {
            var row = await rowService.GetAsync(id);
            return this.HATEOASResult(row, (r) => Ok(r));
        }

        // GET: api/row
        [HttpGet("list/{tableId}", Name = RouteNames.RowGetByTable)]
        public async Task<HATEOASResult> GetByTableAsync(Guid tableId)
        {
            var rows = await rowService.GetRowsAsync(tableId);
            return this.HATEOASResult(rows, (r) => Ok(r));
        }

        // POST: api/row
        [HttpPost("update", Name = RouteNames.RowUpdate)]
        public async Task<HATEOASResult> UpdateAsync([FromBody] Row row)
        {
            var updatedRow = await rowService.UpdateAsync(row);
            return this.HATEOASResult(updatedRow, (r) => Ok(r));
        }

        // POST: api/row
        [HttpPost("create", Name = RouteNames.RowCreate)]
        public async Task<HATEOASResult> CreateAsync([FromBody] Row row)
        {
            var createdRow = await rowService.CreateAsync(row);
            return this.HATEOASResult(createdRow, (r) => Ok(r));
        }

        // POST: api/row
        [HttpPost("delete/{id}", Name = RouteNames.RowDelete)]
        public async Task<HATEOASResult> DeleteAsync(Guid id)
        {
            await rowService.DeleteAsync(id);
            return this.HATEOASResult(null, (r) => Ok(r));
        }
    }
}
