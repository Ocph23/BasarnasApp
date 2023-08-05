using BasarnasApp.Server.Data;
using BasarnasApp.Server.Models;
using BasarnasApp.Server.Services.ServiceContracts;
using BasarnasApp.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
				.Include(x => x.JenisKejadian);
			if (await _userManager.IsInRoleAsync(user, "Instansi"))
			{
				var pihakTerkait = _dbcontext.PihakTerkait.AsNoTracking()
									 .Include(x => x.District)
									 .FirstOrDefault(x => x.UserId == userid);
				queryable = queryable.Where(x => x.District.Id == pihakTerkait.District.Id);
			}
			return queryable.AsEnumerable();
		}

		public Task<Kejadian> PostAsync(Kejadian t)
		{
			try
			{
				_dbcontext.Entry(t.JenisKejadian).State = EntityState.Unchanged;
				_dbcontext.Entry(t.District).State = EntityState.Unchanged;
				_dbcontext.Entry(t.Pelapor).State = EntityState.Unchanged;

				var result = _dbcontext.Kejadian.Add(t);
				_dbcontext.SaveChanges();
				var data = _dbcontext.Kejadian.AsNoTracking()
					.Include(x => x.JenisKejadian)
					.Include(x => x.District)
					.Include(x => x.Pelapor)
					.Include(x => x.PihakTerkait).ThenInclude(x => x.Instansi)
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
	}
}
