using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using MudBlazor;

namespace BasarnasApp.Client.Services;

public class InstansiService : IInstansiService
{

    string controller = "api/Instansi";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;
    private static IEnumerable<InstansiRequest>? _sources = Enumerable.Empty<InstansiRequest>();
    public InstansiService(HttpClient httpClient, ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
    }

    public async Task<IEnumerable<InstansiRequest>> GetAsync()
    {
        try
        {
            if(!_sources.Any())
            {
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
}