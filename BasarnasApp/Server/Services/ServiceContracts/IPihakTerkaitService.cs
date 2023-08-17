using BasarnasApp.Server.Models;
using BasarnasApp.Shared.Models;

namespace BasarnasApp.Server.Services.ServiceContracts
{
    public interface IPihakTerkaitService
    {
        Task<IEnumerable<PihakTerkait>> GetAsync();
        Task<PihakTerkait> GetByIdAsync(int id);
        Task<PihakTerkait> PostAsync(PihakTerkait t, string password);
        Task<bool> PutAsync(int id, PihakTerkait t);
        Task<bool> DeleteAsync(int id);
        Task<PihakTerkait> GetProfile(string? userName);
        Task<bool> ChangePassword(string id, ChangeUserPasswordRequest t);
    }
}
