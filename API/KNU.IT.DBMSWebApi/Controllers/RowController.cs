﻿using KNU.IT.DbManager.Models;
using KNU.IT.DBMSWebApi.Constants;
using KNU.IT.DbServices.Converters;
using KNU.IT.DbServices.Models;
using KNU.IT.DbServices.Services.RowService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet("{id}", Name = RouteNames.RowGet)]
        [ProducesResponseType(typeof(Row), 200)]
        public async Task<HATEOASResult> GetAsync(Guid id)
        {
            var row = await rowService.GetAsync(id);
            var rowResponse = RowConverter.GetRowResponse(row);
            return this.HATEOASResult(rowResponse, (r) => Ok(r));
        }

        // GET: api/row
        [HttpGet("list/{tableId}", Name = RouteNames.RowGetByTable)]
        [ProducesResponseType(typeof(Row), 200)]
        public async Task<HATEOASResult> GetByTableAsync(Guid tableId)
        {
            var rows = await rowService.GetAllAsync(tableId);
            var rowsResponse = rows.Select(r => RowConverter.GetRowResponse(r)).ToList();
            return this.HATEOASResult(rowsResponse, (r) => Ok(r));
        }

        // POST: api/row
        [HttpPut(Name = RouteNames.RowUpdate)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RowResponse>> UpdateAsync([FromBody] Row row)
        {
            if(!await rowService.ValidateAsync(row))
            {
                return BadRequest();
            }
            var updatedRow = await rowService.UpdateAsync(row);
            var rowResponse = RowConverter.GetRowResponse(updatedRow);
            return Ok(rowResponse);
        }

        // POST: api/row
        [HttpPost(Name = RouteNames.RowCreate)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<RowResponse>> CreateAsync([FromBody] Row row)
        {
            if (!await rowService.ValidateAsync(row))
            {
                return BadRequest();
            }
            var createdRow = await rowService.CreateAsync(row);
            var rowResponse = RowConverter.GetRowResponse(createdRow);
            return Ok(rowResponse);
        }

        // POST: api/row
        [HttpDelete("{id}", Name = RouteNames.RowDelete)]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await rowService.DeleteAsync(id);
            return Ok();
        }
    }
}
