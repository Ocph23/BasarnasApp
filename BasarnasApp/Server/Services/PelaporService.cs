using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using BasarnasApp.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasarnasApp.Server.Services
{
    public class PelaporService : IPelaporService
    {
        private readonly ApplicationDbContext _dbcontext;
		private readonly UserManager<ApplicationUser> _userManager;
		public PelaporService(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager)
		{
			_dbcontext = dbcontext;
			_userManager = userManager;
		}


        public async Task<bool> ChangePassword(string id, ChangeUserPasswordRequest t)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                //if (user.Email.ToLower() != t.Email.ToLower())
                //{
                //    var emailConfirmationCode = await _userManager.GenerateChangeEmailTokenAsync(user,t.Email);
                //    var changeResult=  await _userManager.ChangeEmailAsync(user, t.Email, emailConfirmationCode);
                //}

                var changeResult = await _userManager.ChangePasswordAsync(user, t.OldPassword, t.NewPassword);
                if (changeResult.Succeeded)
                {
                    return true;
                }


                var err = changeResult.Errors.FirstOrDefault();
                if (err != null)
                    throw new SystemException(err.Description);
                throw new SystemException("Password Tidak Berhasil Diubah.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = _dbcontext.Pelapor.Where(x => x.Id == id).ExecuteDelete();
                return Task.FromResult(data>0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Pelapor>> GetAsync()
        {
            try
            {
                var result = _dbcontext.Pelapor;
                if (result.Any())
                {
                    return Task.FromResult(result.AsEnumerable());
                }
                return Task.FromResult(Enumerable.Empty<Pelapor>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Pelapor> GetByIdAsync(int id)
        {
            try
            {
                var result = _dbcontext.Pelapor.SingleOrDefault(x => x.Id == id);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Pelapor> GetProfile(string? userid)
        {
            try
            {
                var result = _dbcontext.Pelapor
                    .SingleOrDefault(x => x.UserId == userid);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Pelapor> PostAsync(Pelapor t)
        {
			var user = new ApplicationUser(t.Email) { Email = t.Email, EmailConfirmed = true };
			try
			{
				var userCreated = await _userManager.CreateAsync(user, t.Password);
				if (userCreated.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "Pelapor");
					t.UserId = user.Id;
					_dbcontext.Pelapor.Add(t);
					var result = _dbcontext.SaveChanges();
					if (result > 0)
					{
                        t.Password= string.Empty;
						return t;
					}
				}
				throw new SystemException(String.Join(",", userCreated.Errors.Select(x=>x.Description).ToArray()));
			}
			catch (Exception)
			{
                var x = await _userManager.FindByEmailAsync(t.Email);
                if(x is not null)
                {
				    await _userManager.DeleteAsync(user);
                }
				throw;
			}
		}

        public Task<bool> PutAsync(int id, Pelapor t)
        {
            try
            {
                var result = _dbcontext.Pelapor.Where(x => x.Id == t.Id).ExecuteUpdate(
                    x => x
                    .SetProperty(x => x.Name, t.Name)
                    .SetProperty(x=>x.PhoneNumber, t.PhoneNumber)
                    .SetProperty(x=>x.Photo, t.Photo)
                    .SetProperty(x=>x.Thumb, t.Thumb)
					.SetProperty(x => x.Address, t.Address));
                return Task.FromResult(result > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
