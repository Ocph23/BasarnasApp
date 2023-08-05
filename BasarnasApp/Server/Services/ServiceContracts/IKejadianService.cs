using BasarnasApp.Server.Models;
using BasarnasApp.Shared;

namespace BasarnasApp.Server.Services.ServiceContracts
{
    public interface IKejadianService
    {
        Task<IEnumerable<Kejadian>> GetAsync();
        Task<Kejadian> GetByIdAsync(int id);
        Task<Kejadian> PostAsync(Kejadian t);
        Task<bool> PutAsync(int id, Kejadian t);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Kejadian>> GetByUserAsync(string userid);
        Task<bool> ChangeStatus(int id, StatusLaporan request);
    }
}
