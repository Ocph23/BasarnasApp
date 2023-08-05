using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasarnasApp.Server.Services
{
    public class PihakTerkaitService : IPihakTerkaitService
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;

        public PihakTerkaitService(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }

        public Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = _dbcontext.PihakTerkait
                    .Where(x => x.Id == id).ExecuteDelete();
                return Task.FromResult(data>0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<PihakTerkait>> GetAsync()
        {
            try
            {
                var result = _dbcontext.PihakTerkait.Include(x => x.Instansi)
                    .Include(x => x.District);
                if (result.Any())
                {
                    return Task.FromResult(result.AsEnumerable());
                }
                return Task.FromResult(Enumerable.Empty<PihakTerkait>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<PihakTerkait> GetByIdAsync(int id)
        {
            try
            {
                var result = _dbcontext.PihakTerkait
                     .Include(x => x.Instansi)
                    .Include(x => x.District)
                    .SingleOrDefault(x => x.Id == id);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PihakTerkait> GetProfile(string? userid)
        {
            try
            {
                var result = _dbcontext.PihakTerkait
                     .Include(x => x.Instansi)
                    .Include(x => x.District)
                    .SingleOrDefault(x => x.UserId == userid);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PihakTerkait> PostAsync(PihakTerkait t)
        {
            try
            {
                var user = new ApplicationUser(t.Email) { Email = t.Email,  EmailConfirmed = true };
                var userCreated = await _userManager.CreateAsync(user, "Password@123");
                if (userCreated.Succeeded)
                {
                   await _userManager.AddToRoleAsync(user, "Instansi");

                    t.UserId = user.Id;

                    _dbcontext.Entry(t.District).State = EntityState.Unchanged;
                    _dbcontext.Entry(t.Instansi).State = EntityState.Unchanged;

                    _dbcontext.PihakTerkait.Add(t);
                    var result = _dbcontext.SaveChanges();
                    if(result<=0)
                    {
                       await _userManager.DeleteAsync(user);
                    }
                    else
                    {
                        return t;
                    }
                }
                throw new SystemException("User Tidak berhasil dibuat !");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> PutAsync(int id, PihakTerkait t)
        {
            try
            {
                var result = _dbcontext.PihakTerkait.Where(x => x.Id == t.Id).ExecuteUpdate(
                    x => x
                    .SetProperty(x => x.Name, t.Name)
                    .SetProperty(x => x.Description, t.Description));
                return Task.FromResult(result > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
