using BasarnasApp.Shared.Models;

namespace BasarnasApp.Client.Services
{
     public interface IPihakTerkaitService
    {
        Task<IEnumerable<PihakTerkaitRequest>> GetAsync();
        Task<PihakTerkaitRequest> GetByIdAsync(int id);
        Task<PihakTerkaitRequest> PostAsync(PihakTerkaitRequest t);
        Task<bool> PutAsync(int id, PihakTerkaitRequest t);
        Task<bool> DeleteAsync(int id);
        Task<PihakTerkaitRequest> GetProfile();
    }
}
