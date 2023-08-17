using BasarnasApp.Server.Data;
using BasarnasApp.Server.Services.ServiceContracts;
using BasarnasApp.Shared.Models;

namespace BasarnasApp.Server.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbcontext;

        public DashboardService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public Task<DashboardModel> GetAsync()
        {
            try
            {
                var model = new DashboardModel();

                model.Pelapor = _dbcontext.Pelapor.Count();
                model.Instansi = _dbcontext.Instansi.Count();
                model.Laporan = _dbcontext.Kejadian.Count();
                model.PihakTerkait = _dbcontext.PihakTerkait.Count();
                model.District = _dbcontext.Districts.Count();
                return Task.FromResult(model);
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
