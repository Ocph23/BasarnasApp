using BasarnasApp.Shared.Models;

namespace BasarnasApp.Client.Services
{
     public interface IInstansiService
    {
        Task<IEnumerable<InstansiRequest>> GetAsync();
        Task<InstansiRequest> GetByIdAsync(int id);
        Task<InstansiRequest> PostAsync(InstansiRequest t);
        Task<bool> PutAsync(int id, InstansiRequest t);
        Task<bool> DeleteAsync(int id);
    }
}
