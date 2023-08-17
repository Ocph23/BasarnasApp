using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;
using OcphApiAuth.Client;

namespace BasarnasApp.Client.Services;

public class InstansiService : IInstansiService
{

    string controller = "api/Instansi";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;
    private static IEnumerable<InstansiRequest>? _sources = Enumerable.Empty<InstansiRequest>();
    private ILocalStorageService _localStorageService;

    public InstansiService(HttpClient httpClient, ISnackbar snackbar, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
        _localStorageService = localStorageService;
    }

    public async Task<IEnumerable<InstansiRequest>> GetAsync()
    {
        try
        {

            if(!_sources.Any())
            {
                await _httpClient.SetToken(_localStorageService);
                _sources = await _httpClient.GetFromJsonAsync<IEnumerable<InstansiRequest>>($"{controller}");
            }
            return _sources!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return Enumerable.Empty<InstansiRequest>();    
        }
    }

    public async Task<InstansiRequest> GetByIdAsync(int id)
    {
       try
        {
            var data = await _httpClient.GetFromJsonAsync<InstansiRequest>($"{controller}/{id}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return null;    
        }
    }

    public async Task<InstansiRequest> PostAsync(InstansiRequest t)
    {
       try
        {
            var response = await _httpClient.PostAsJsonAsync<InstansiRequest>($"{controller}",t);
            if(response.IsSuccessStatusCode){
                _snackbar.Add("Data Berhasil Disimpan.", Severity.Success);
                return await response.GetResult<InstansiRequest>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return default(InstansiRequest);
        }
    }

    public async Task<bool> PutAsync(int id, InstansiRequest t)
    {
       try
        {
            var response = await _httpClient.PutAsJsonAsync<InstansiRequest>($"{controller}/{id}",t);
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