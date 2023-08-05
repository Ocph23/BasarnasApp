using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
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

        public async Task<Pelapor> GetProfile(string? userid)
        {
            try
            {
                var result = _dbcontext.Pelapor
                    .SingleOrDefault(x => x.UserId == userid);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return result;
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
