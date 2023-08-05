using BasarnasApp.Shared.Models;

namespace BasarnasMobilaApp.Services
{
     public interface IJenisKejadianService
    {
        Task<IEnumerable<JenisKejadianRequest>> GetAsync();
        Task<JenisKejadianRequest> GetByIdAsync(int id);
        Task<JenisKejadianRequest> PostAsync(JenisKejadianRequest t);
        Task<bool> PutAsync(int id, JenisKejadianRequest t);
        Task<bool> DeleteAsync(int id);
    }
}
