using OtomasyonApi.DTOs;

namespace OtomasyonApi.Services
{
    public interface IFaturaService
    {
        Task<int> CreateAsync(FaturaCreateDto dto);
        Task DeleteAsync(int id);
        Task<FaturaDetailDto?> GetAsync(int id);
        Task<List<FaturaDto>> GetAllAsync(string? faturaTipi = null);
        Task UpdateAsync(int id, FaturaCreateDto dto);
    }
}

