using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CIPlatform.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message, string userId)
        {
            await Clients.All.SendAsync("ReceiveMsg", message,userId);
        }
    }
}
