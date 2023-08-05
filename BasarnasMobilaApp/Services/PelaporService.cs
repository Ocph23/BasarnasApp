using System.Net.Http.Json;
using BasarnasMobilaApp.Models;

namespace BasarnasMobilaApp.Services;

public class PelaporService : IPelaporService
{
    string controller = "api/pelapor";
    private readonly RestClient _httpClient;

    public PelaporService(RestClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Pelapor>> GetAsync()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<Pelapor>>($"{controller}");
            return data!;
        }
        catch (Exception ex)
        {
           
           return Enumerable.Empty<Pelapor>();    
        }
    }

    public async Task<Pelapor> GetByIdAsync(int id)
    {
       try
        {
            var data = await _httpClient.GetFromJsonAsync<Pelapor>($"{controller}/{id}");
            return data!;
        }
        catch (Exception ex)
        {
           
           return null;    
        }
    }

    public async Task<Pelapor> PostAsync(Pelapor t)
    {
       try
        {
            var response = await _httpClient.PostAsJsonAsync<Pelapor>($"{controller}",t);
            if(response.IsSuccessStatusCode){
                return await response.GetResult<Pelapor>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           
           return default(Pelapor);
        }
    }

    public async Task<bool> PutAsync(int id, Pelapor t)
    {
       try
        {
            var response = await _httpClient.PutAsJsonAsync<Pelapor>($"{controller}/{id}",t);
            if(response.IsSuccessStatusCode){
                return await response.GetResult<bool>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           
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
           
           return false;
        }
    }
}