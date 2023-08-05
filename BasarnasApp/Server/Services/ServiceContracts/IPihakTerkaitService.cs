using BasarnasApp.Server.Models;

namespace BasarnasApp.Server.Services.ServiceContracts
{
    public interface IPihakTerkaitService
    {
        Task<IEnumerable<PihakTerkait>> GetAsync();
        Task<PihakTerkait> GetByIdAsync(int id);
        Task<PihakTerkait> PostAsync(PihakTerkait t);
        Task<bool> PutAsync(int id, PihakTerkait t);
        Task<bool> DeleteAsync(int id);
        Task<PihakTerkait> GetProfile(string? userName);
    }
}
