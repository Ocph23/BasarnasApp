
using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;

namespace BasarnasMobilaApp.Services
{
     public interface IKejadianService
    {
        Task<IEnumerable<KejadianRequest>> GetAsync();
        Task<KejadianRequest> GetByIdAsync(int id);
        Task<KejadianRequest> PostAsync(KejadianRequest t);
        Task<bool> PutAsync(int id, KejadianRequest t);
        Task<bool> DeleteAsync(int id);
        Task<KejadianRequest> GetProfile();
        Task<bool> ChangeStatus(int kejId, StatusLaporan status);
    }
}
