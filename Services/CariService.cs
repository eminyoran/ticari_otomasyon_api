using Microsoft.EntityFrameworkCore;
using OtomasyonApi.Data;
using OtomasyonApi.DTOs;
using OtomasyonApi.Models;

namespace OtomasyonApi.Services
{
    public class CariService : ICariService
    {
        private readonly AppDbContext _context;

        public CariService(AppDbContext context)
        {
            _context = context;
        }

        // CARİ KODU (CR00001)
        private async Task<string> GenerateCariCode()
        {
            int count = await _context.Cariler.CountAsync();
            return $"CR{(count + 1).ToString("D5")}";
        }

        public async Task<int> CreateAsync(CariCreateDto dto)
        {
            var cari = new Cari
            {
                CariKodu = await GenerateCariCode(),
                Unvan = dto.Unvan ?? "",
                TicariUnvan = dto.TicariUnvan ?? "",
                VergiDairesi = dto.VergiDairesi ?? "",
                VergiNo = dto.VergiNo ?? "",
                TcNo = dto.TcNo ?? "",
                Telefon = dto.Telefon ?? "",
                Telefon2 = dto.Telefon2 ?? "",
                Email = dto.Email ?? "",
                Adres = dto.Adres ?? "",
                Ulke = dto.Ulke ?? "",
                Sehir = dto.Sehir ?? "",
                Ilce = dto.Ilce ?? "",
                Iban = dto.Iban ?? "",
                CariTipi = dto.CariTipi ?? "",
                ParaBirimi = dto.ParaBirimi ?? "",
                Durum = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Cariler.Add(cari);
            await _context.SaveChangesAsync();
            return cari.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var cari = await _context.Cariler.FindAsync(id);
            if (cari == null) return;

            _context.Cariler.Remove(cari);
            await _context.SaveChangesAsync();
        }

        public async Task<CariDetailDto?> GetAsync(int id)
        {
            var entity = await _context.Cariler
                .Include(x => x.Hareketler)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            return new CariDetailDto
            {
                Id = entity.Id,
                CariKodu = entity.CariKodu,
                Unvan = entity.Unvan,
                TicariUnvan = entity.TicariUnvan,
                VergiDairesi = entity.VergiDairesi,
                VergiNo = entity.VergiNo,
                TcNo = entity.TcNo,
                Telefon = entity.Telefon,
                Telefon2 = entity.Telefon2,
                Email = entity.Email,
                Adres = entity.Adres,
                Ulke = entity.Ulke,
                Sehir = entity.Sehir,
                Ilce = entity.Ilce,
                Iban = entity.Iban,
                CariTipi = entity.CariTipi,
                ParaBirimi = entity.ParaBirimi,
                Durum = entity.Durum,
                Bakiye = entity.Hareketler.Sum(x => (decimal)(x.Alacak - x.Borc)),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<List<CariDto>> GetAllAsync()
        {
            var list = await _context.Cariler
                .Include(x => x.Hareketler)
                .ToListAsync();

            return list.Select(entity => new CariDto
            {
                Id = entity.Id,
                CariKodu = entity.CariKodu,
                Unvan = entity.Unvan,
                Telefon = entity.Telefon,
                Email = entity.Email,
                Sehir = entity.Sehir,
                Ilce = entity.Ilce,
                Bakiye = entity.Hareketler.Sum(x => (decimal)(x.Alacak - x.Borc))
            }).ToList();
        }

        public async Task UpdateAsync(int id, CariCreateDto dto)
        {
            var cari = await _context.Cariler.FindAsync(id);
            if (cari == null) return;

            cari.Unvan = dto.Unvan ?? cari.Unvan;
            cari.TicariUnvan = dto.TicariUnvan ?? cari.TicariUnvan;
            cari.VergiDairesi = dto.VergiDairesi ?? cari.VergiDairesi;
            cari.VergiNo = dto.VergiNo ?? cari.VergiNo;
            cari.TcNo = dto.TcNo ?? cari.TcNo;
            cari.Telefon = dto.Telefon ?? cari.Telefon;
            cari.Telefon2 = dto.Telefon2 ?? cari.Telefon2;
            cari.Email = dto.Email ?? cari.Email;
            cari.Adres = dto.Adres ?? cari.Adres;
            cari.Ulke = dto.Ulke ?? cari.Ulke;
            cari.Sehir = dto.Sehir ?? cari.Sehir;
            cari.Ilce = dto.Ilce ?? cari.Ilce;
            cari.Iban = dto.Iban ?? cari.Iban;
            cari.CariTipi = dto.CariTipi ?? cari.CariTipi;
            cari.ParaBirimi = dto.ParaBirimi ?? cari.ParaBirimi;
            cari.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
