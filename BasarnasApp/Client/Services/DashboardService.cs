using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;
using OcphApiAuth.Client;

namespace BasarnasApp.Client.Services;

public class DashboardService : IDashboardService
{

    string controller = "api/dashboard";
    private ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;

    public DashboardService(HttpClient httpClient, ISnackbar snackbar, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
        _localStorageService = localStorageService;
    }

    public async Task<DashboardModel> GetAsync()
    {
        try
        {
            await _httpClient.SetToken(_localStorageService);
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