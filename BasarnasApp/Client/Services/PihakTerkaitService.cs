using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;

namespace BasarnasApp.Client.Services;

public class PihakTerkaitService : IPihakTerkaitService
{

    string controller = "api/pihakterkait";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;

    public PihakTerkaitService(HttpClient httpClient, ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
    }

    public async Task<IEnumerable<PihakTerkaitRequest>> GetAsync()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<PihakTerkaitRequest>>($"{controller}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return Enumerable.Empty<PihakTerkaitRequest>();    
        }
    }

    public async Task<PihakTerkaitRequest> GetByIdAsync(int id)
    {
       try
        {
            var data = await _httpClient.GetFromJsonAsync<PihakTerkaitRequest>($"{controller}/{id}");
            return data!;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return null;    
        }
    }

    public async Task<PihakTerkaitRequest> PostAsync(PihakTerkaitRequest t)
    {
       try
        {
            var response = await _httpClient.PostAsJsonAsync<PihakTerkaitRequest>($"{controller}",t);
            if(response.IsSuccessStatusCode){
                return await response.GetResult<PihakTerkaitRequest>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return default(PihakTerkaitRequest);
        }
    }

    public async Task<bool> PutAsync(int id, PihakTerkaitRequest t)
    {
       try
        {
            var response = await _httpClient.PutAsJsonAsync<PihakTerkaitRequest>($"{controller}/{id}",t);
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

    public async Task<PihakTerkaitRequest> GetProfile()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<PihakTerkaitRequest>($"{controller}/profile");
            return data!;
        }
        catch (Exception ex)
        {
            _snackbar.Add(ex.Message, Severity.Error);
            return null;
        }
    }
}