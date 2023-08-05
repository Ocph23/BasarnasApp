using System.Net.Http.Json;
using BasarnasApp.Shared.Models;

namespace BasarnasMobilaApp.Services;

public class JenisKejadianService : IJenisKejadianService
{

    string controller = "api/jeniskejadian";
    private readonly RestClient _httpClient;
    private static IEnumerable<JenisKejadianRequest> _sources = Enumerable.Empty<JenisKejadianRequest>();

    public JenisKejadianService(RestClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<JenisKejadianRequest>> GetAsync()
    {
        try
        {
            if(!_sources.Any())
            {
                var data = await _httpClient.GetFromJsonAsync<IEnumerable<JenisKejadianRequest>>($"{controller}");
                if (data != null)
                {
                    _sources = data;
                }
            }
            return _sources;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<JenisKejadianRequest> GetByIdAsync(int id)
    {
       try
        {
            var data = await _httpClient.GetFromJsonAsync<JenisKejadianRequest>($"{controller}/{id}");
            return data!;
        }
        catch (Exception)
        {
           throw;
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
        catch (Exception)
        {
           throw;
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
            throw new SystemException(await response.GetError());
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
}