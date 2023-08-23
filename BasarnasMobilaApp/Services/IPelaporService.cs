using BasarnasMobilaApp.Models;
using BasarnasMobilaApp.Pages;

namespace BasarnasMobilaApp.Services
{
     public interface IPelaporService
    {
        Task<IEnumerable<Pelapor>> GetAsync();
        Task<Pelapor> GetByIdAsync(int id);
        Task<Pelapor> PostAsync(Pelapor t);
        Task<bool> PutAsync(int id, Pelapor t);
        Task<bool> DeleteAsync(int id);
        Task<bool> ChangePassword(ChangePassword password);
    }
}
