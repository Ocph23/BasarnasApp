using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace BasarnasApp.Server.Services
{
    public class InstansiService : IInstansiService
    {
        private readonly ApplicationDbContext _dbcontext;

        public InstansiService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = _dbcontext.Instansi.Where(x => x.Id == id).ExecuteDelete();
                return Task.FromResult(data > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Instansi>> GetAsync()
        {
            try
            {
                var result = _dbcontext.Instansi;
                if (result.Any())
                {
                    return Task.FromResult(result.AsEnumerable());
                }
                return Task.FromResult(Enumerable.Empty<Instansi>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Instansi> GetByIdAsync(int id)
        {
            try
            {
                var result = _dbcontext.Instansi.SingleOrDefault(x => x.Id == id);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Instansi> PostAsync(Instansi t)
        {
            try
            {
                var result = _dbcontext.Instansi.Add(t);
                _dbcontext.SaveChanges();
                return Task.FromResult(t);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> PutAsync(int id, Instansi t)
        {
            try
            {
                var result = _dbcontext.Instansi.Where(x => x.Id == t.Id).ExecuteUpdate(
                    x => x
                    .SetProperty(x => x.Name, t.Name)
                    .SetProperty(x => x.Logo, t.Logo)
                    .SetProperty(x => x.Thumb, t.Thumb)
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
