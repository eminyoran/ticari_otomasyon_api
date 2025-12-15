using OtomasyonApi.DTOs;

namespace OtomasyonApi.Services
{
    public interface IKasaService
    {
        Task<int> CreateAsync(KasaCreateDto dto);
        Task DeleteAsync(int id);
        Task<KasaDetailDto?> GetAsync(int id);
        Task<List<KasaDto>> GetAllAsync();
        Task UpdateAsync(int id, KasaCreateDto dto);
    }
}

