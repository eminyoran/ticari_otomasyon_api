using OtomasyonApi.DTOs;

namespace OtomasyonApi.Services
{
    public interface ICariService
    {
        Task<int> CreateAsync(CariCreateDto dto);
        Task DeleteAsync(int id);
        Task<CariDetailDto?> GetAsync(int id);
        Task<List<CariDto>> GetAllAsync();
        Task UpdateAsync(int id, CariCreateDto dto);
    }
}
