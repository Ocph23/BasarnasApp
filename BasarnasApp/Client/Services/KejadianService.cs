using System.Net.Http.Json;
using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;
using OcphApiAuth.Client;

namespace BasarnasApp.Client.Services;

public class KejadianService : IKejadianService
{

    string controller = "api/kejadian";
    private ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;

    public KejadianService(HttpClient httpClient, ISnackbar snackbar, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
        _localStorageService = localStorageService;
    }

    public async Task<IEnumerable<KejadianRequest>> GetAsync()
    {
        try
        {
            await _httpClient.SetToken(_localStorageService);
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
                _snackbar.Add("Data Berhasil Disimpan.", Severity.Success);
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
            if (response)
            {
                _snackbar.Add("Data Berhasil Diubah.", Severity.Success);
            }
             return response;
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return false;
        }
    }

    public async Task<IEnumerable<PenangananRequest>> GetPenanganan(int kejId)
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<PenangananRequest>>($"{controller}/penanganan/{kejId}");
            return data!;
        }
        catch (Exception ex)
        {
            _snackbar.Add(ex.Message, Severity.Error);
            return Enumerable.Empty<PenangananRequest>();
        }
    }

    public async Task<bool> UpdatePenanganan(PenangananRequest t)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync<PenangananRequest>($"{controller}/penanganan/{t.Id}", t);
            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add("Data Berhasil Diubah.", Severity.Success);
                return await response.GetResult<bool>();
            }
            throw new SystemException(await response.GetError());
        }
        catch (Exception ex)
        {
            _snackbar.Add(ex.Message, Severity.Error);
            return false;
        }
    }

    public async Task<IEnumerable<PenangananModel>> GePenanganan()
    {
        try
        {
            await _httpClient.SetToken(_localStorageService);
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<PenangananModel>>($"{controller}/penanganan");
            return data!;
        }
        catch (Exception ex)
        {
            _snackbar.Add(ex.Message, Severity.Error);
            return Enumerable.Empty<PenangananModel>();
        }
    }

    public async Task<IEnumerable<PenangananModel>> GePenangananByPihakId(int pihakId)
    {
        try
        {
            await _httpClient.SetToken(_localStorageService);
            var data = await _httpClient.GetFromJsonAsync<IEnumerable<PenangananModel>>($"{controller}/penangananreport/{pihakId}");
            return data!;
        }
        catch (Exception ex)
        {
            _snackbar.Add(ex.Message, Severity.Error);
            return Enumerable.Empty<PenangananModel>();
        }
    }
}