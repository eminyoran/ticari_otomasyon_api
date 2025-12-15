using Microsoft.EntityFrameworkCore;
using OtomasyonApi.Data;
using OtomasyonApi.DTOs;
using OtomasyonApi.Models;

namespace OtomasyonApi.Services
{
    public class KasaService : IKasaService
    {
        private readonly AppDbContext _context;

        public KasaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(KasaCreateDto dto)
        {
            var kasa = new Kasa
            {
                KasaAdi = dto.KasaAdi ?? "",
                KasaKodu = dto.KasaKodu,
                Bakiye = dto.Bakiye ?? 0,
                ParaBirimi = dto.ParaBirimi ?? "TRY",
                Aciklama = dto.Aciklama,
                Durum = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Kasalar.Add(kasa);
            await _context.SaveChangesAsync();
            return kasa.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var kasa = await _context.Kasalar.FindAsync(id);
            if (kasa == null) return;

            _context.Kasalar.Remove(kasa);
            await _context.SaveChangesAsync();
        }

        public async Task<KasaDetailDto?> GetAsync(int id)
        {
            var entity = await _context.Kasalar.FindAsync(id);
            if (entity == null) return null;

            return new KasaDetailDto
            {
                Id = entity.Id,
                KasaAdi = entity.KasaAdi,
                KasaKodu = entity.KasaKodu,
                Bakiye = entity.Bakiye,
                ParaBirimi = entity.ParaBirimi,
                Aciklama = entity.Aciklama,
                Durum = entity.Durum,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<List<KasaDto>> GetAllAsync()
        {
            var list = await _context.Kasalar.ToListAsync();

            return list.Select(entity => new KasaDto
            {
                Id = entity.Id,
                KasaAdi = entity.KasaAdi,
                KasaKodu = entity.KasaKodu,
                Bakiye = entity.Bakiye,
                ParaBirimi = entity.ParaBirimi,
                Durum = entity.Durum
            }).ToList();
        }

        public async Task UpdateAsync(int id, KasaCreateDto dto)
        {
            var kasa = await _context.Kasalar.FindAsync(id);
            if (kasa == null) return;

            kasa.KasaAdi = dto.KasaAdi ?? kasa.KasaAdi;
            kasa.KasaKodu = dto.KasaKodu ?? kasa.KasaKodu;
            kasa.Bakiye = dto.Bakiye ?? kasa.Bakiye;
            kasa.ParaBirimi = dto.ParaBirimi ?? kasa.ParaBirimi;
            kasa.Aciklama = dto.Aciklama ?? kasa.Aciklama;
            kasa.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}

