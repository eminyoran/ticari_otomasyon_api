using OtomasyonApi.Models;

namespace OtomasyonApi.Repositories
{
    public interface ICariHareketRepository
    {
        Task<IEnumerable<CariHareket>> GetByCariIdAsync(int cariId);
        Task<CariHareket?> GetByIdAsync(int id);
        Task<int> CreateAsync(CariHareket hareket);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CariHareket>> GetListAsync(int? cariId, DateTime? baslangic, DateTime? bitis);
    }
}
