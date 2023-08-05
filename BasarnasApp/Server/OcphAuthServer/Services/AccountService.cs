using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace OcphApiAuth
{
    public class AccountService : IAccountService
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext dbcontext;

        public AccountService(IOptions<AppSettings> appSettings,
            UserManager<ApplicationUser>
            _userManager, SignInManager<ApplicationUser> _signInManager, ApplicationDbContext _dbcontext)
        {
            _appSettings = appSettings.Value;
            userManager = _userManager;
            signInManager = _signInManager;
            dbcontext = _dbcontext;
        }

        public async Task<AuthenticateResponse> Login(LoginRequest model)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName.ToUpper(), model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    ApplicationUser? user = await userManager.FindByEmailAsync(model.UserName);
                    var roles = await userManager.GetRolesAsync(user);
                    var token = await user.GenerateToken(_appSettings, roles);
                    return new AuthenticateResponse(user.UserName, user.Email, token);

                }
                throw new SystemException($"Your Account {model.UserName} Not Have Access !");
            }
            catch (System.Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<AuthenticateResponse> Register(RegisterRequest requst)
        {
            try
            {
                ApplicationUser user = new ApplicationUser
                {
                    PhoneNumber = requst.PhoneNumber,
                    Email = requst.Email,
                    UserName = requst.Email,
                    EmailConfirmed = true
                };
                var userCreated = await userManager.CreateAsync(user, requst.Password);
                if (userCreated.Succeeded)
                {
                    if (!string.IsNullOrEmpty(requst.Role))
                    {
                        await userManager.AddToRoleAsync(user, requst.Role);
                    }
                    var token = await user.GenerateToken(_appSettings, new List<string> { requst.Role });
                    return new AuthenticateResponse(user.UserName, user.Email, token);
                }

                string errorMessage = string.Empty;
                if (userCreated.Errors.Count() > 0)
                {
                    errorMessage = userCreated.Errors.FirstOrDefault().Description;
                }

                throw new SystemException(errorMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<ApplicationUser> FindUserById(string id)
        {
            throw new NotImplementedException();
        }


        public Task AddUserRole(string v, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> FindUserByUserName(string userName) => await userManager.FindByNameAsync(userName);

        public Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            throw new NotImplementedException();
        }
        public Task<ApplicationUser> FindUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }


}
