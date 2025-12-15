using OtomasyonApi.DTOs;
using OtomasyonApi.Models;
using OtomasyonApi.Repositories;

namespace OtomasyonApi.Services
{
    public class CariHareketService : ICariHareketService
    {
        private readonly ICariHareketRepository _repo;

        public CariHareketService(ICariHareketRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CariHareketListDto>> GetListAsync(
            int? cariId,
            DateTime? baslangic,
            DateTime? bitis)
        {
            var hareketler = await _repo.GetListAsync(cariId, baslangic, bitis);

            return hareketler.Select(h => new CariHareketListDto
            {
                Id = h.Id,
                CariId = h.CariId,
                Tarih = h.Tarih,
                Aciklama = h.Aciklama,
                Borc = h.Borc,
                Alacak = h.Alacak,
                EvrakTipi = h.EvrakTipi,
                ReferansId = h.ReferansId,
                KasaId = h.KasaId,
                BankaId = h.BankaId,
                Doviz = h.Doviz,
                Kur = h.Kur,
                CreatedAt = h.CreatedAt
            });
        }

        public async Task<int> CreateAsync(CariHareketCreateDto dto)
        {
            var hareket = new CariHareket
            {
                CariId = dto.CariId,
                Tarih = dto.Tarih,
                Aciklama = dto.Aciklama,
                Borc = dto.Borc,
                Alacak = dto.Alacak,
                EvrakTipi = dto.EvrakTipi,
                ReferansId = dto.ReferansId,
                KasaId = dto.KasaId,
                BankaId = dto.BankaId,
                Doviz = dto.Doviz,
                Kur = dto.Kur,
                CreatedAt = DateTime.UtcNow
            };

            return await _repo.CreateAsync(hareket);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
