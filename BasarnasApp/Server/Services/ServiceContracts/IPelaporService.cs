using BasarnasApp.Server.Models;

namespace BasarnasApp.Server.Services.ServiceContracts
{
    public interface IPelaporService
    {
        Task<IEnumerable<Pelapor>> GetAsync();
        Task<Pelapor> GetByIdAsync(int id);
        Task<Pelapor> PostAsync(Pelapor t);
        Task<bool> PutAsync(int id, Pelapor t);
        Task<bool> DeleteAsync(int id);
        Task<Pelapor> GetProfile(string? userName);
    }
}
