using Microsoft.AspNetCore.SignalR.Client;

namespace BasarnasMobilaApp
{
    public interface ISignalRMessageService
    {
        Task Start();
    }

    public class SignalRMessageService : ISignalRMessageService
    {
        private static HubConnection hubConnection;

        public async Task Start()
        {
            hubConnection = new HubConnectionBuilder()
          .WithUrl(Helper.Url + "/apphub")
          .Build();

            await Task.Delay(3000);
            hubConnection.On<string, string>("ReceiveMessage", async (user, message) =>
            {

            });

            await hubConnection.StartAsync();
        }
    }

}
