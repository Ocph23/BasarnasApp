using BasarnasApp.Server.Models;
using BasarnasApp.Shared;
using BasarnasApp.Shared.Models;

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
        Task<IEnumerable<Penanganan>> GetPenanganan(int kejId);
        Task<IEnumerable<PenangananModel>> GePenanganan();
        Task<IEnumerable<PenangananModel>> GePenangananByPihakId(int pihakId);
        Task<bool> UpdatePenanganan(Penanganan model);
    }
}
