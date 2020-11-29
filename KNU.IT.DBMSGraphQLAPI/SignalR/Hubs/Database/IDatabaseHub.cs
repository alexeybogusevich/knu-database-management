using System.Threading.Tasks;

namespace KNU.IT.DBMSGraphQLAPI.SignalR.Hubs
{
    public interface IDatabaseHub
    {
        Task SendMessageAsync(string message);
    }
}
