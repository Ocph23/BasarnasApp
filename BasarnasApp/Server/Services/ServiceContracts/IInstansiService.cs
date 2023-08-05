using BasarnasApp.Server.Models;

namespace BasarnasApp.Server.Services.ServiceContracts
{
    public interface IInstansiService
    {
        Task<IEnumerable<Instansi>> GetAsync();
        Task<Instansi> GetByIdAsync(int id);
        Task<Instansi> PostAsync(Instansi t);
        Task<bool> PutAsync(int id, Instansi t);
        Task<bool> DeleteAsync(int id);
    }
}
