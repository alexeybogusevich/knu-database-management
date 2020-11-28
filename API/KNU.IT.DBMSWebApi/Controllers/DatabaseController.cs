using KNU.IT.DbManager.Models;
using KNU.IT.DBMSWebApi.Constants;
using KNU.IT.DbServices.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        [HttpGet("{id}", Name = RouteNames.DatabaseGet)]
        [ProducesResponseType(typeof(Database), 200)]
        public async Task<HATEOASResult> GetAsync(Guid id)
        {
            var database = await databaseService.GetAsync(id);
            return this.HATEOASResult(database, (d) => Ok(d));
        }

        // GET: api/database
        [HttpGet(Name = RouteNames.DatabaseGetAll)]
        [ProducesResponseType(typeof(Database), 200)]
        public async Task<HATEOASResult> GetAllAsync()
        {
            var databases = await databaseService.GetAllAsync();
            return this.HATEOASResult(databases, (d) => Ok(d));
        }

        // POST: api/database
        [HttpPost(Name = RouteNames.DatabaseCreate)]
        [ProducesResponseType(200)]
        public async Task<HATEOASResult> CreateAsync([FromBody] Database database)
        {
            var createdDatabase = await databaseService.CreateAsync(database);
            return this.HATEOASResult(createdDatabase, (d) => Ok(d));
        }

        // POST: api/database
        [HttpPut(Name = RouteNames.DatabaseUpdate)]
        [ProducesResponseType(200)]
        public async Task<HATEOASResult> UpdateAsync([FromBody] Database database)
        {
            var updatedDatabase = await databaseService.CreateAsync(database);
            return this.HATEOASResult(updatedDatabase, (d) => Ok(d));
        }

        // POST: api/database
        [HttpDelete("{id}", Name = RouteNames.DatabaseDelete)]
        [ProducesResponseType(200)]
        public async Task<HATEOASResult> DeleteAsync([FromQuery] Guid id)
        {
            await databaseService.DeleteAsync(id);
            return this.HATEOASResult(null, (d) => Ok());
        }
    }
}
