using BasarnasApp.Client;
using BasarnasApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor.Services;
using OcphApiAuth.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BasarnasApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
   ;// .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BasarnasApp.ServerAPI"));

//builder.Services.AddApiAuthorization();

builder.Services.AddMudServices();

builder.Services.AddOcphAuthClient();


builder.Services.AddSingleton<SignalRService>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<IInstansiService, InstansiService>();
builder.Services.AddScoped<IJenisKejadianService, JenisKejadianService>();
builder.Services.AddScoped<IPihakTerkaitService, PihakTerkaitService>();
builder.Services.AddScoped<IKejadianService, KejadianService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();



await builder.Build().RunAsync();
