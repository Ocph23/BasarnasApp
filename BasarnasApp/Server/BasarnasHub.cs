using System.Text.Json;
using BasarnasApp.Server.Models;
using Microsoft.AspNetCore.SignalR;

namespace BasarnasApp.Server
{
    public class BasarnasHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

     

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

    }
}
