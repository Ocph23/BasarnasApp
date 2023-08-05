using System.Net.Http.Json;
using BasarnasApp.Shared.Models;

namespace BasarnasMobilaApp.Services;

public class DistrictService : IDistrictService
{

    string controller = "api/district";
    private readonly RestClient _httpClient;

    private List<DistrictRequest> _districtRequests = new List<DistrictRequest>();


    public DistrictService(RestClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<DistrictRequest>> GetAsync()
    {
        try
        {
            if (_districtRequests.Count <= 0)
            {
                var data = await _httpClient.GetFromJsonAsync<IEnumerable<DistrictRequest>>($"{controller}");
                if (data != null)
                {
                    _districtRequests = data.ToList();
                }
            }
            return _districtRequests;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<DistrictRequest> GetByIdAsync(int id)
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<DistrictRequest>($"{controller}/{id}");
            return data!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<DistrictRequest> PostAsync(DistrictRequest t)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<DistrictRequest>($"{controller}", t);
            if (response.IsSuccessStatusCode)
            {
                return await response.GetResult<DistrictRequest>();
            }
            throw new SystemException(await response.GetError());
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> PutAsync(int id, DistrictRequest t)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync<DistrictRequest>($"{controller}/{id}", t);
            if (response.IsSuccessStatusCode)
            {
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