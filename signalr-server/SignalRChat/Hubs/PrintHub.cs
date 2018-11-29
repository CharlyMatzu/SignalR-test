using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class PrintHub : Hub
    {
        public async Task SendNotify(string url)
        {
            await Clients.All.SendAsync("Notification", url);
        }
    }
}

