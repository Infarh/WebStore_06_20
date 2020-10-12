using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebStore.Hubs
{
    public class InformationHub : Hub
    {
        public async Task SendMessage(string Message) => await Clients.All.SendAsync("MessageFromClient", Message);
    }
}
