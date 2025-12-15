using OtomasyonApi.DTOs;

namespace OtomasyonApi.Services
{
    public interface IBankaService
    {
        Task<int> CreateAsync(BankaCreateDto dto);
        Task DeleteAsync(int id);
        Task<BankaDetailDto?> GetAsync(int id);
        Task<List<BankaDto>> GetAllAsync();
        Task UpdateAsync(int id, BankaCreateDto dto);
    }
}

