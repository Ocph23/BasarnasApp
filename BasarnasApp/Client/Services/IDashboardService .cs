using BasarnasApp.Shared.Models;

namespace BasarnasApp.Client.Services
{
     public interface IDashboardService
    {
        Task<DashboardModel> GetAsync();
    }
}
