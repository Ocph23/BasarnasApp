using System.Net.Http.Json;
using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;

namespace BasarnasApp.Client.Services;

public class KejadianService : IKejadianService
{

    string controller = "api/kejadian";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;

    public KejadianService(HttpClient httpClient, ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
    }

    public async Task<IEnumerable<KejadianRequest>> GetAsync()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<KejadianRequest>>($"{controller}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return Enumerable.Empty<KejadianRequest>();    
        }
    }

    public async Task<KejadianRequest> GetByIdAsync(int id)
    {
       try
        {
            var data = await _httpClient.GetFromJsonAsync<KejadianRequest>($"{controller}/{id}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return null;    
        }
    }

    public async Task<KejadianRequest> PostAsync(KejadianRequest t)
    {
       try
        {
            var response = await _httpClient.PostAsJsonAsync<KejadianRequest>($"{controller}",t);
            if(response.IsSuccessStatusCode){
                return await response.GetResult<KejadianRequest>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return default(KejadianRequest);
        }
    }

    public async Task<bool> PutAsync(int id, KejadianRequest t)
    {
       try
        {
            var response = await _httpClient.PutAsJsonAsync<KejadianRequest>($"{controller}/{id}",t);
            if(response.IsSuccessStatusCode){
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
             return deleted;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return false;
        }
    }

    public async Task<KejadianRequest> GetProfile()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<KejadianRequest>($"{controller}/profile");
            return data!;
        }
        catch (Exception ex)
        {
            _snackbar.Add(ex.Message, Severity.Error);
            return null;
        }
    }

    public async Task<bool> ChangeStatus(int kejId, StatusLaporan status)
    {
        try
        {
            int xstatus = (int)status;
            var response = await _httpClient.GetFromJsonAsync<bool>($"{controller}/changestatus/{kejId}/{xstatus}");
             return response;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return false;
        }
    }
}