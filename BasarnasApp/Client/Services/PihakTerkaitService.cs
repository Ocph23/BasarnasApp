using System.Net.Http.Json;
using BasarnasApp.Shared.Models;
using Blazored.LocalStorage;
using MudBlazor;
using OcphApiAuth.Client;

namespace BasarnasApp.Client.Services;

public class PihakTerkaitService : IPihakTerkaitService
{

    string controller = "api/pihakterkait";
    private readonly HttpClient _httpClient;
    private readonly ISnackbar _snackbar;
    private readonly ILocalStorageService _localStorage;

    public PihakTerkaitService(HttpClient httpClient, ISnackbar snackbar, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _snackbar = snackbar;
        _localStorage = localStorage;
    }

    public async Task<IEnumerable<PihakTerkaitRequest>> GetAsync()
    {
        try
        {
            await _httpClient.SetToken(_localStorage);
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
           return default!;    
        }
    }

    public async Task<PihakTerkaitRequest> PostAsync(PihakTerkaitRequest t)
    {
       try
        {
            var response = await _httpClient.PostAsJsonAsync<PihakTerkaitRequest>($"{controller}",t);
            if(response.IsSuccessStatusCode){
                _snackbar.Add("Data Berhasil Disimpan.", Severity.Success);
                return await response.GetResult<PihakTerkaitRequest>();
            }
            throw new SystemException( await response.GetError());
        }
        catch (Exception ex)
        {
           _snackbar.Add(ex.Message, Severity.Error);
           return default!;
        }
    }

    public async Task<bool> PutAsync(int id, PihakTerkaitRequest t)
    {
       try
        {
            var response = await _httpClient.PutAsJsonAsync<PihakTerkaitRequest>($"{controller}/{id}",t);
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

    public async Task<PihakTerkaitRequest> GetProfile()
    {
        try
        {
            var data = await _httpClient.GetFromJsonAsync<PihakTerkaitRequest>($"{controller}/profile");
            if(data!=null){
                await  _localStorage.SetItemAsync("profile", data);
            }
         
            return data!;
        }
        catch (Exception ex)
        {
            _snackbar.Add(ex.Message, Severity.Error);
            return default!;
        }
    }

    public async Task<bool> ChangePassword(string id, ChangeUserPasswordRequest t)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync<ChangeUserPasswordRequest>($"{controller}/changepassword/{id}", t);
            if (response.IsSuccessStatusCode)
            {
                _snackbar.Add("Data Berhasil Disimpan.", Severity.Success);
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
}