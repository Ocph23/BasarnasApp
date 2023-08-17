using BasarnasApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Text.Json;

namespace BasarnasApp.Client.Services
{
    public class SignalRService
    {
        private readonly HubConnection? hubConnection;

        public event Action<KejadianRequest>? OnKejadianChange;


        public SignalRService(NavigationManager Navigation, IJSRuntime js)
        {


            hubConnection = new HubConnectionBuilder()
                  .WithUrl(Navigation.ToAbsoluteUri("/apphub"))
                  .Build();

            hubConnection.On<string>("KejadianMessage", async (message) =>
            {
                try
                {
                    var kejadian = JsonSerializer.Deserialize<KejadianRequest>(message);
                    OnKejadianChange?.Invoke(kejadian!);
                    var data = $"Telah terjadi {kejadian!.JenisKejadianName} di {kejadian.DistrictName} dan dilaporkan oleh : {kejadian.PelaporName}";
                    await js.InvokeVoidAsync("OnKejadianFire", data);
                }
                catch (Exception)
                {

                }

            });

            _ = Start();
        }

        private async Task Start()
        {
            await hubConnection.StartAsync();
        }

        //public Task SetProfile(PihakTerkaitRequest profile)
        //{
        //    return Task.CompletedTask;
        //}
    }
}