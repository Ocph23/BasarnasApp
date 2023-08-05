using BasarnasApp.Server.Models;
using OcphApiAuth;

namespace OcphApiAuth
{
    public interface IAccountService
    {
        Task<ApplicationUser> FindUserById(string id);
        Task<AuthenticateResponse> Login(LoginRequest model);
        Task<AuthenticateResponse> Register(RegisterRequest requst);
        Task AddUserRole(string v, ApplicationUser user);
        Task<ApplicationUser> FindUserByUserName(string userName);
        Task<ApplicationUser> FindUserByEmail(string email);
        Task<IEnumerable<ApplicationUser>> GetUsers();
    }

}
