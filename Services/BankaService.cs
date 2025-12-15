using Microsoft.EntityFrameworkCore;
using OtomasyonApi.Data;
using OtomasyonApi.DTOs;
using OtomasyonApi.Models;

namespace OtomasyonApi.Services
{
    public class BankaService : IBankaService
    {
        private readonly AppDbContext _context;

        public BankaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(BankaCreateDto dto)
        {
            var banka = new Banka
            {
                BankaAdi = dto.BankaAdi ?? "",
                SubeAdi = dto.SubeAdi,
                HesapNo = dto.HesapNo,
                Iban = dto.Iban,
                Bakiye = dto.Bakiye ?? 0,
                ParaBirimi = dto.ParaBirimi ?? "TRY",
                Aciklama = dto.Aciklama,
                Durum = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Bankalar.Add(banka);
            await _context.SaveChangesAsync();
            return banka.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var banka = await _context.Bankalar.FindAsync(id);
            if (banka == null) return;

            _context.Bankalar.Remove(banka);
            await _context.SaveChangesAsync();
        }

        public async Task<BankaDetailDto?> GetAsync(int id)
        {
            var entity = await _context.Bankalar.FindAsync(id);
            if (entity == null) return null;

            return new BankaDetailDto
            {
                Id = entity.Id,
                BankaAdi = entity.BankaAdi,
                SubeAdi = entity.SubeAdi,
                HesapNo = entity.HesapNo,
                Iban = entity.Iban,
                Bakiye = entity.Bakiye,
                ParaBirimi = entity.ParaBirimi,
                Aciklama = entity.Aciklama,
                Durum = entity.Durum,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<List<BankaDto>> GetAllAsync()
        {
            var list = await _context.Bankalar.ToListAsync();

            return list.Select(entity => new BankaDto
            {
                Id = entity.Id,
                BankaAdi = entity.BankaAdi,
                SubeAdi = entity.SubeAdi,
                HesapNo = entity.HesapNo,
                Iban = entity.Iban,
                Bakiye = entity.Bakiye,
                ParaBirimi = entity.ParaBirimi,
                Durum = entity.Durum
            }).ToList();
        }

        public async Task UpdateAsync(int id, BankaCreateDto dto)
        {
            var banka = await _context.Bankalar.FindAsync(id);
            if (banka == null) return;

            banka.BankaAdi = dto.BankaAdi ?? banka.BankaAdi;
            banka.SubeAdi = dto.SubeAdi ?? banka.SubeAdi;
            banka.HesapNo = dto.HesapNo ?? banka.HesapNo;
            banka.Iban = dto.Iban ?? banka.Iban;
            banka.Bakiye = dto.Bakiye ?? banka.Bakiye;
            banka.ParaBirimi = dto.ParaBirimi ?? banka.ParaBirimi;
            banka.Aciklama = dto.Aciklama ?? banka.Aciklama;
            banka.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}

