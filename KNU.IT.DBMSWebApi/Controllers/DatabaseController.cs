using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Services.DatabaseService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KNU.IT.DBMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseService databaseService;

        public DatabaseController(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        // GET: api/database
        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var database = await databaseService.GetAsync(id);
            return Ok(database);
        }

        // GET: api/database
        [HttpGet("list")]
        public async Task<ActionResult> GetAllAsync()
        {
            var databases = await databaseService.GetAllAsync();
            return Ok(databases);
        }

        // POST: api/database
        [HttpPost("create")]
        public async Task<ActionResult> CreateAsync([FromBody] Database database)
        {
            var createdDatabase = await databaseService.CreateAsync(database);
            return Ok(createdDatabase);
        }

        // POST: api/database
        [HttpPost("update")]
        public async Task<ActionResult> UpdateAsync([FromBody] Database database)
        {
            var createdDatabase = await databaseService.CreateAsync(database);
            return Ok(createdDatabase);
        }

        // POST: api/database
        [HttpPost("delete/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await databaseService.DeleteAsync(id);
            return Ok();
        }
    }
}
