using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;
using OcphApiAuth.Client;

namespace BasarnasApp.Client.Services;

public class DashboardService : IDashboardService
{

    string controller = "api/dashboard";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;

    public DashboardService(HttpClient httpClient, ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
    }

    public async Task<DashboardModel> GetAsync()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<DashboardModel>($"{controller}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
            throw;
        }
    }

}