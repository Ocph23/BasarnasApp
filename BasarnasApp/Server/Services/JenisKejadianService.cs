using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace BasarnasApp.Server.Services
{
    public class JenisKejadianervice : IJenisKejadianService
    {
        private readonly ApplicationDbContext _dbcontext;

        public JenisKejadianervice(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = _dbcontext.JenisKejadian.Where(x => x.Id == id).ExecuteDelete();
                return Task.FromResult(data>0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<JenisKejadian>> GetAsync()
        {
            try
            {
                var result = _dbcontext.JenisKejadian
                                .Include(x => x.JenisInstansi)
                                .ThenInclude(x => x.Instansi);
                if (result.Any())
                {
                    return Task.FromResult(result.AsEnumerable());
                }
                return Task.FromResult(Enumerable.Empty<JenisKejadian>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<JenisKejadian> GetByIdAsync(int id)
        {
            try
            {
                var result = _dbcontext.JenisKejadian
                                .Include(x => x.JenisInstansi)
                                .ThenInclude(x => x.Instansi)
                                .SingleOrDefault(x => x.Id == id);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<JenisKejadian> PostAsync(JenisKejadian t)
        {
            try
            {
                foreach (var item in t.JenisInstansi)
                {
                    _dbcontext.Entry(item.Instansi).State = EntityState.Unchanged;
                }
                var result = _dbcontext.JenisKejadian.Add(t);
                _dbcontext.SaveChanges();
                return Task.FromResult(t);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> PutAsync(int id, JenisKejadian t)
        {
            try
            {
                var result = _dbcontext.JenisKejadian.SingleOrDefault(x => x.Id == id);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan !");
                _dbcontext.Entry(result).CurrentValues.SetValues(t);
                _dbcontext.SaveChangesAsync();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
