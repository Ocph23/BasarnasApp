using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using MudBlazor;

namespace BasarnasApp.Client.Services;

public class JenisKejadianService : IJenisKejadianService
{

    string controller = "api/jeniskejadian";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;
    private static IEnumerable<JenisKejadianRequest> _sources;


    public JenisKejadianService(HttpClient httpClient, ISnackbar snackbar)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
    }

    public async Task<IEnumerable<JenisKejadianRequest>> GetAsync()
    {
        try
        {
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
                return await response.GetResult<JenisKejadianRequest>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return default(JenisKejadianRequest);
        }
    }

    public async Task<bool> PutAsync(int id, JenisKejadianRequest t)
    {
       try
        {
            var response = await _httpClient.PutAsJsonAsync<JenisKejadianRequest>($"{controller}/{id}",t);
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