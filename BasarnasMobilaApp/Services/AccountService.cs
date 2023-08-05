using BasarnasMobilaApp.Models;
using System.Net.Http.Json;

namespace BasarnasMobilaApp.Services
{
    public class AccountService : IAccountService
    {
        private RestClient httpClient;

        public AccountService(
            RestClient _clientFactory)
        {
            httpClient = _clientFactory;
        }
        public async Task<AuthenticateResponse> Login(LoginRequest model)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/account/login", new LoginRequest(model.UserName, model.Password));
                if (response.IsSuccessStatusCode)
                {
                    var contentString = await response.Content.ReadAsStringAsync();
                    AuthenticateResponse result = await response.GetResult<AuthenticateResponse>();
                    if (result != null)
                    {
                        return result;
                    }
                }
                throw new SystemException(await response.GetError());
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task Logout()
        {
            await Account.LogOut();
        }

        public async Task<AuthenticateResponse> Register(RegisterRequest register)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/account/register", register);
                if (response.IsSuccessStatusCode)
                {
                    var contentString = await response.Content.ReadAsStringAsync();
                    AuthenticateResponse result = await response.GetResult<AuthenticateResponse>();
                    if (result != null)
                    {
                        return result;
                    }
                }
                throw new SystemException(await response.GetError());
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
}
