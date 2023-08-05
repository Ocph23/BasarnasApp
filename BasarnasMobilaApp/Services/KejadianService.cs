using System.Net.Http.Json;
using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;

namespace BasarnasMobilaApp.Services;

public class KejadianService : IKejadianService
{

    string controller = "api/kejadian";
    private readonly RestClient _httpClient;

    public KejadianService(RestClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<KejadianRequest>> GetAsync()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<KejadianRequest>>($"{controller}");
            return data!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<KejadianRequest> GetByIdAsync(int id)
    {
       try
        {
            var data = await _httpClient.GetFromJsonAsync<KejadianRequest>($"{controller}/{id}");
            return data!;
        }
        catch (Exception)
        {
           throw;
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
        catch (Exception )
        {
           throw;
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
        catch (Exception)
        {
           throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
       try
        {
            var deleted = await _httpClient.DeleteFromJsonAsync<bool>($"{controller}/{id}");
             return deleted;
        }
        catch (Exception)
        {
           throw;
        }
    }

    public async Task<KejadianRequest> GetProfile()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<KejadianRequest>($"{controller}/profile");
            return data!;
        }
        catch (Exception)
        {
            throw;
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
        catch (Exception)
        {
           throw;
        }
    }
}