using OtomasyonApi.DTOs;

namespace OtomasyonApi.Services
{
    public interface ICariHareketService
    {
        Task<IEnumerable<CariHareketListDto>> GetListAsync(
            int? cariId,
            DateTime? baslangic,
            DateTime? bitis);

        Task<int> CreateAsync(CariHareketCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
