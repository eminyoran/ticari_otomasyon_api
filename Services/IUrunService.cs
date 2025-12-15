using OtomasyonApi.DTOs;

namespace OtomasyonApi.Services
{
    public interface IUrunService
    {
        Task<int> CreateAsync(UrunCreateDto dto);
        Task DeleteAsync(int id);
        Task<UrunDetailDto?> GetAsync(int id);
        Task<List<UrunDto>> GetAllAsync();
        Task UpdateAsync(int id, UrunCreateDto dto);
    }
}

