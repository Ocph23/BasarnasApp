using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasarnasApp.Server.Services
{
    public class KejadianService : IKejadianService
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        public KejadianService(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager = null)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
        }

        public Task<bool> ChangeStatus(int id, StatusLaporan request)
        {
            try
            {
                var result = _dbcontext.Kejadian.Where(x => x.Id == id).ExecuteUpdate(
                    x => x
                    .SetProperty(x => x.Status, request));
                return Task.FromResult(result > 0);
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
                var data = _dbcontext.Kejadian.Where(x => x.Id == id).ExecuteDelete();
                return Task.FromResult(data > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<Kejadian>> GetAsync()
        {
            try
            {
                var result = _dbcontext.Kejadian;
                if (result.Any())
                {
                    return Task.FromResult(result.AsEnumerable());
                }
                return Task.FromResult(Enumerable.Empty<Kejadian>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Kejadian> GetByIdAsync(int id)
        {
            try
            {
                var result = _dbcontext.Kejadian.SingleOrDefault(x => x.Id == id);
                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");
                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Kejadian>> GetByUserAsync(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            IQueryable<Kejadian> queryable = _dbcontext.Kejadian
                .Include(x => x.District)
                .Include(x => x.Pelapor)
                .Include(x => x.JenisKejadian).ThenInclude(x=>x.JenisInstansi).ThenInclude(x=>x.Instansi);
            if (await _userManager.IsInRoleAsync(user, "Instansi"))
            {
                var pihakTerkait = _dbcontext.PihakTerkait.AsNoTracking()
                                     .Include(x => x.District)
                                     .FirstOrDefault(x => x.UserId == userid);
                queryable = queryable.Where(x => x.District.Id == pihakTerkait.District.Id);
            }
            return queryable.AsEnumerable();
        }

        public Task<IEnumerable<Penanganan>> GetPenanganan(int kejId)
        {
            try
            {
                Kejadian? result = _dbcontext.Kejadian.Where(x => x.Id == kejId)
                    .Include(x => x.Penanganan).ThenInclude(x => x.Instansi)
                    .Include(x => x.Penanganan).ThenInclude(x => x.PihakTerkait)
                    .SingleOrDefault(x => x.Id == kejId);

                ArgumentNullException.ThrowIfNull(result, "Data Tidak Ditemukan.");

                return Task.FromResult(result.Penanganan.AsEnumerable());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Kejadian> PostAsync(Kejadian t)
        {
            try
            {
                var jenisKejadian = _dbcontext.JenisKejadian.AsNoTracking().Where(x => x.Id == t.JenisKejadian.Id)
                    .Include(x => x.JenisInstansi)
                    .ThenInclude(x => x.Instansi);

                var xx = jenisKejadian.FirstOrDefault();

                foreach (var item in xx.JenisInstansi)
                {
                    _dbcontext.Entry(item.Instansi).State = EntityState.Unchanged;
                    t.Penanganan.Add(new Penanganan { Instansi = item.Instansi });
                }

                _dbcontext.Entry(t.JenisKejadian).State = EntityState.Unchanged;
                _dbcontext.Entry(t.District).State = EntityState.Unchanged;
                _dbcontext.Entry(t.Pelapor).State = EntityState.Unchanged;

                var result = _dbcontext.Kejadian.Add(t);
                _dbcontext.SaveChanges();
                var data = _dbcontext.Kejadian.AsNoTracking()
                    .Include(x => x.JenisKejadian)
                    .Include(x => x.District)
                    .Include(x => x.Pelapor)
                    .Include(x => x.Penanganan).ThenInclude(x => x.PihakTerkait)
                    .FirstOrDefault(x => x.Id == t.Id);

                return Task.FromResult(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> PutAsync(int id, Kejadian t)
        {
            try
            {
                var result = _dbcontext.Kejadian.Where(x => x.Id == t.Id).ExecuteUpdate(
                    x => x
                    .SetProperty(x => x.Status, t.Status));
                return Task.FromResult(result > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> UpdatePenanganan(Penanganan model)
        {
            try
            {
                var kejadian = (from a in _dbcontext.Kejadian
                                .Include(x => x.Penanganan).ThenInclude(x => x.PihakTerkait)
                                from b in a.Penanganan.Where(x => x.Id == model.Id)
                                select a).SingleOrDefault();
                var penanganan = kejadian.Penanganan.SingleOrDefault(x => x.Id == model.Id);

                ArgumentNullException.ThrowIfNull(penanganan, "Data penanganan tidak ditemukan.");

                penanganan.Status = model.Status;
                penanganan.Penyebab = model.Penyebab;
                penanganan.Lokasi = model.Lokasi;
                penanganan.Deskripsi = model.Deskripsi;
                if (penanganan.PihakTerkait == null)
                {
                    penanganan.PihakTerkait = model.PihakTerkait;
                    _dbcontext.Entry(penanganan.PihakTerkait).State = EntityState.Unchanged;
                }

                if (kejadian.Status != StatusLaporan.Selesai && model.Status == StatusPenganan.Ditangani)
                {
                    kejadian.Status = StatusLaporan.Ditangani;
                }

                if (kejadian.Status != StatusLaporan.Selesai && model.Status == StatusPenganan.Selesai)
                {
                    if (kejadian.Penanganan.Where(x => x.Status == StatusPenganan.Selesai).Count() == kejadian.Penanganan.Count)
                    {
                        kejadian.Status = StatusLaporan.Selesai;
                    }

                }

                var saved = _dbcontext.SaveChanges();
                return Task.FromResult(saved > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<PenangananModel>> GePenangananByPihakId(int pihakId)
        {
            try
            {
                var penanganan = from a in _dbcontext.Kejadian.Include(x => x.Penanganan)
                               .ThenInclude(x => x.PihakTerkait)
                               .ThenInclude(x => x.Instansi)
                                 from b in a.Penanganan.Where(x => x.PihakTerkait.Id == pihakId)
                                 select new PenangananModel
                                 {
                                     InstansiId = b.Instansi.Id,
                                     PihakTerkaitId = b.PihakTerkait.Id,
                                     Penyebab = b.Penyebab,
                                     Lokasi = b.Lokasi,
                                     Deskripsi = b.Deskripsi,
                                     Tanggal = a.Tanggal,
                                     KejadianId = a.Id,
                                     Id = b.Id
                                 };
                return Task.FromResult(penanganan.AsEnumerable());

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<PenangananModel>> GePenanganan()
        {
            try
            {
                var penanganan = from a in _dbcontext.Kejadian.Include(x => x.Penanganan)
                                 .ThenInclude(x => x.Instansi)
                                 from b in a.Penanganan
                                 select new PenangananModel
                                 {
                                     InstansiId = b.Instansi.Id,
                                     KejadianId = a.Id,
                                     InstansiName = b.Instansi.Name,
                                     Penyebab = b.Penyebab,
                                     Lokasi = b.Lokasi,
                                     Deskripsi = b.Deskripsi,
                                     Tanggal = a.Tanggal,
                                     Id = b.Id
                                 };
                return Task.FromResult(penanganan.AsEnumerable());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
