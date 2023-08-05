using BasarnasApp.Server.Models;

namespace BasarnasApp.Server.Services.ServiceContracts
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> GetAsync();
        Task<District> GetByIdAsync(int id);
        Task<District> PostAsync(District t);
        Task<bool> PutAsync(int id, District t);
        Task<bool> DeleteAsync(int id);
    }
}
