using KNU.IT.DbManager.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KNU.IT.DBMSGraphQLAPI.SignalR.Hubs
{
    public class DatabaseHub : Hub, IDatabaseHub
    {
        private readonly IHubContext<DatabaseHub> hubContext;

        public DatabaseHub(IHubContext<DatabaseHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task SendMessageAsync(string message)
        {
            await hubContext.Clients.All.SendAsync(nameof(Database), message);
        }
    }
}
