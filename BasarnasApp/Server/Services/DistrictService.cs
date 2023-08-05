using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace BasarnasApp.Server.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly ApplicationDbContext _dbcontext;

        public DistrictService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = _dbcontext.Districts.Where(x => x.Id == id).ExecuteDelete();
                return Task.FromResult(data>0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<District>> GetAsync()
        {
            try
            {
                var result = _dbcontext.Districts;
                if (result.Any())
                {
                    return Task.FromResult(result.AsEnumerable());
                }
                return Task.FromResult(Enumerable.Empty<District>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<District> GetByIdAsync(int id)
        {
            try
            {
                var result = _dbcontext.Districts.SingleOrDefault(x => x.Id == id);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<District> PostAsync(District t)
        {
            try
            {
                var result = _dbcontext.Districts.Add(t);
                _dbcontext.SaveChanges();
                return Task.FromResult(t);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> PutAsync(int id, District t)
        {
            try
            {
                var result = _dbcontext.Districts.Where(x => x.Id == t.Id).ExecuteUpdate(
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
