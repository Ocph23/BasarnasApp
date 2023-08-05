using BasarnasApp.Server.Models;
using BasarnasApp.Shared.Models;

namespace BasarnasApp.Server.Services.ServiceContracts
{
    public interface IDashboardService
    {
        Task<DashboardModel> GetAsync();
    }
}                                                                                                 