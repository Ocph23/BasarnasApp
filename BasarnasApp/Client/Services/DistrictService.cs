using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;
using OcphApiAuth.Client;

namespace BasarnasApp.Client.Services;

public class DistrictService : IDistrictService
{

    string controller = "api/district";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;
    private readonly ILocalStorageService _localStorageService;

    public DistrictService(HttpClient httpClient, ISnackbar snackbar, ILocalStorageService localStorageService = null)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
        _localStorageService = localStorageService;
    }

    public async Task<IEnumerable<DistrictRequest>> GetAsync()
    {
        try
        {
           await _httpClient.SetToken(_localStorageService);
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<DistrictRequest>>($"{controller}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return Enumerable.Empty<DistrictRequest>();    
        }
    }

    public async Task<DistrictRequest> GetByIdAsync(int id)
    {
       try
        {
            var data = await _httpClient.GetFromJsonAsync<DistrictRequest>($"{controller}/{id}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return null;    
        }
    }

    public async Task<DistrictRequest> PostAsync(DistrictRequest t)
    {
       try
        {
            var response = await _httpClient.PostAsJsonAsync<DistrictRequest>($"{controller}",t);
            if(response.IsSuccessStatusCode){
                _snackbar.Add("Data Berhasil Disimpan.", Severity.Success);
                return await response.GetResult<DistrictRequest>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return default(DistrictRequest);
        }
    }

    public async Task<bool> PutAsync(int id, DistrictRequest t)
    {
       try
        {
            var response = await _httpClient.PutAsJsonAsync<DistrictRequest>($"{controller}/{id}",t);
            if(response.IsSuccessStatusCode){
                _snackbar.Add("Data Berhasil Disimpan.", Severity.Success);
                return await response.GetResult<bool>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
       try
        {
            var deleted = await _httpClient.DeleteFromJsonAsync<bool>($"{controller}/{id}");
            if (deleted)
            {
                _snackbar.Add("Data Berhasil Dihapus.", Severity.Success);
            }
             return deleted;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return false;
        }
    }
}