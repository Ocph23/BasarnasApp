using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;
using OcphApiAuth.Client;

namespace BasarnasApp.Client.Services;

public class JenisKejadianService : IJenisKejadianService
{

    readonly string controller = "api/jeniskejadian";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;
    private readonly ILocalStorageService _localStorageService;

    public JenisKejadianService(HttpClient httpClient, ISnackbar snackbar, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
        _localStorageService = localStorageService;
    }

    public async Task<IEnumerable<JenisKejadianRequest>> GetAsync()
    {
        try
        {
            await _httpClient.SetToken(_localStorageService);
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<JenisKejadianRequest>>($"{controller}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return Enumerable.Empty<JenisKejadianRequest>();    
        }
    }

    public async Task<JenisKejadianRequest> GetByIdAsync(int id)
    {
       try
        {
            var data = await _httpClient.GetFromJsonAsync<JenisKejadianRequest>($"{controller}/{id}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return null;    
        }
    }

    public async Task<JenisKejadianRequest> PostAsync(JenisKejadianRequest t)
    {
       try
        {
            var response = await _httpClient.PostAsJsonAsync<JenisKejadianRequest>($"{controller}",t);
            if(response.IsSuccessStatusCode){
                _snackbar.Add("Data Berhasil Disimpan.", Severity.Success);
                return await response.GetResult<JenisKejadianRequest>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
            return default!;
        }
    }

    public async Task<bool> PutAsync(int id, JenisKejadianRequest t)
    {
       try
        {
            var response = await _httpClient.PutAsJsonAsync<JenisKejadianRequest>($"{controller}/{id}",t);
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