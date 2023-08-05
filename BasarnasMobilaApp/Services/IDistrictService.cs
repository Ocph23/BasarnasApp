using BasarnasApp.Shared.Models;

namespace BasarnasMobilaApp.Services
{
     public interface IDistrictService
    {
        Task<IEnumerable<DistrictRequest>> GetAsync();
        Task<DistrictRequest> GetByIdAsync(int id);
        Task<DistrictRequest> PostAsync(DistrictRequest t);
        Task<bool> PutAsync(int id, DistrictRequest t);
        Task<bool> DeleteAsync(int id);
    }
}
