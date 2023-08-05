using BasarnasApp.Server.Models;

namespace BasarnasApp.Server.Services.ServiceContracts
{
    public interface IJenisKejadianService
    {
        Task<IEnumerable<JenisKejadian>> GetAsync();
        Task<JenisKejadian> GetByIdAsync(int id);
        Task<JenisKejadian> PostAsync(JenisKejadian t);
        Task<bool> PutAsync(int id, JenisKejadian t);
        Task<bool> DeleteAsync(int id);
    }
}
