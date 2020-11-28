using KNU.IT.DbManager.Models;
using KNU.IT.DbServices.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KNU.IT.DBMSGraphQLAPI.Controllers
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

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Database>> GetAllAsync()
        {
            var databases = await databaseService.GetAllAsync();
            return Ok(databases);
        }
    }
}
