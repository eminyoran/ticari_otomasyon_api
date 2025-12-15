using Microsoft.EntityFrameworkCore;
using OtomasyonApi.Data;
using OtomasyonApi.DTOs;
using OtomasyonApi.Models;

namespace OtomasyonApi.Services
{
    public class UrunService : IUrunService
    {
        private readonly AppDbContext _context;

        public UrunService(AppDbContext context)
        {
            _context = context;
        }

        private async Task<string> GenerateUrunCode()
        {
            int count = await _context.Urunler.CountAsync();
            return $"UR{(count + 1).ToString("D5")}";
        }

        public async Task<int> CreateAsync(UrunCreateDto dto)
        {
            var urun = new Urun
            {
                UrunKodu = await GenerateUrunCode(),
                Adi = dto.Adi ?? "",
                Barkod = dto.Barkod,
                Birim = dto.Birim,
                BirimFiyat = dto.BirimFiyat,
                KdvOrani = dto.KdvOrani,
                StokMiktari = dto.StokMiktari ?? 0,
                MinStokMiktari = dto.MinStokMiktari,
                Kategori = dto.Kategori,
                Marka = dto.Marka,
                Aciklama = dto.Aciklama,
                Durum = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Urunler.Add(urun);
            await _context.SaveChangesAsync();
            return urun.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null) return;

            _context.Urunler.Remove(urun);
            await _context.SaveChangesAsync();
        }

        public async Task<UrunDetailDto?> GetAsync(int id)
        {
            var entity = await _context.Urunler.FindAsync(id);
            if (entity == null) return null;

            return new UrunDetailDto
            {
                Id = entity.Id,
                UrunKodu = entity.UrunKodu,
                Adi = entity.Adi,
                Barkod = entity.Barkod,
                Birim = entity.Birim,
                BirimFiyat = entity.BirimFiyat,
                KdvOrani = entity.KdvOrani,
                StokMiktari = entity.StokMiktari,
                MinStokMiktari = entity.MinStokMiktari,
                Kategori = entity.Kategori,
                Marka = entity.Marka,
                Aciklama = entity.Aciklama,
                Durum = entity.Durum,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<List<UrunDto>> GetAllAsync()
        {
            var list = await _context.Urunler.ToListAsync();

            return list.Select(entity => new UrunDto
            {
                Id = entity.Id,
                UrunKodu = entity.UrunKodu,
                Adi = entity.Adi,
                Barkod = entity.Barkod,
                Birim = entity.Birim,
                BirimFiyat = entity.BirimFiyat,
                KdvOrani = entity.KdvOrani,
                StokMiktari = entity.StokMiktari,
                MinStokMiktari = entity.MinStokMiktari,
                Kategori = entity.Kategori,
                Marka = entity.Marka,
                Aciklama = entity.Aciklama,
                Durum = entity.Durum,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            }).ToList();
        }

        public async Task UpdateAsync(int id, UrunCreateDto dto)
        {
            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null) return;

            urun.Adi = dto.Adi ?? urun.Adi;
            urun.Barkod = dto.Barkod ?? urun.Barkod;
            urun.Birim = dto.Birim ?? urun.Birim;
            urun.BirimFiyat = dto.BirimFiyat ?? urun.BirimFiyat;
            urun.KdvOrani = dto.KdvOrani ?? urun.KdvOrani;
            urun.StokMiktari = dto.StokMiktari ?? urun.StokMiktari;
            urun.MinStokMiktari = dto.MinStokMiktari ?? urun.MinStokMiktari;
            urun.Kategori = dto.Kategori ?? urun.Kategori;
            urun.Marka = dto.Marka ?? urun.Marka;
            urun.Aciklama = dto.Aciklama ?? urun.Aciklama;
            urun.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}

