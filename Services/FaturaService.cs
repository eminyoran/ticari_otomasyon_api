using Microsoft.EntityFrameworkCore;
using OtomasyonApi.Data;
using OtomasyonApi.DTOs;
using OtomasyonApi.Models;

namespace OtomasyonApi.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly AppDbContext _context;

        public FaturaService(AppDbContext context)
        {
            _context = context;
        }

        private async Task<string> GenerateFaturaNo(string faturaTipi)
        {
            string prefix = faturaTipi == "Satis" ? "ST" : "AL";
            int count = await _context.Faturalar
                .Where(f => f.FaturaTipi == faturaTipi)
                .CountAsync();
            return $"{prefix}{(count + 1).ToString("D6")}";
        }

        public async Task<int> CreateAsync(FaturaCreateDto dto)
        {
            var faturaNo = await GenerateFaturaNo(dto.FaturaTipi ?? "Satis");
            
            var fatura = new Fatura
            {
                FaturaNo = faturaNo,
                FaturaTipi = dto.FaturaTipi ?? "Satis",
                CariId = dto.CariId,
                Tarih = dto.Tarih.HasValue 
                    ? dto.Tarih.Value.Kind == DateTimeKind.Utc 
                        ? dto.Tarih.Value 
                        : dto.Tarih.Value.ToUniversalTime()
                    : DateTime.UtcNow,
                Aciklama = dto.Aciklama,
                Durum = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Önce faturayı kaydet ki Id alalım
            _context.Faturalar.Add(fatura);
            await _context.SaveChangesAsync();

            decimal toplamTutar = 0;
            decimal kdvTutari = 0;

            if (dto.Kalemler != null && dto.Kalemler.Any())
            {
                foreach (var kalemDto in dto.Kalemler)
                {
                    var urun = await _context.Urunler.FindAsync(kalemDto.UrunId);
                    var kdvOrani = kalemDto.KdvOrani ?? urun?.KdvOrani ?? 0;
                    var tutar = kalemDto.Miktar * kalemDto.BirimFiyat;
                    var kdv = tutar * kdvOrani / 100;

                    var kalem = new FaturaKalemi
                    {
                        FaturaId = fatura.Id,
                        UrunId = kalemDto.UrunId,
                        Miktar = kalemDto.Miktar,
                        BirimFiyat = kalemDto.BirimFiyat,
                        KdvOrani = kdvOrani,
                        KdvTutari = kdv,
                        Tutar = tutar
                    };

                    fatura.Kalemler.Add(kalem);
                    toplamTutar += tutar;
                    kdvTutari += kdv;
                }

                fatura.ToplamTutar = toplamTutar;
                fatura.KdvTutari = kdvTutari;
                fatura.GenelToplam = toplamTutar + kdvTutari;

                await _context.SaveChangesAsync();
            }

            return fatura.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var fatura = await _context.Faturalar
                .Include(f => f.Kalemler)
                .FirstOrDefaultAsync(f => f.Id == id);
            
            if (fatura == null) return;

            _context.FaturaKalemleri.RemoveRange(fatura.Kalemler);
            _context.Faturalar.Remove(fatura);
            await _context.SaveChangesAsync();
        }

        public async Task<FaturaDetailDto?> GetAsync(int id)
        {
            var entity = await _context.Faturalar
                .Include(f => f.Cari)
                .Include(f => f.Kalemler)
                    .ThenInclude(k => k.Urun)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (entity == null) return null;

            return new FaturaDetailDto
            {
                Id = entity.Id,
                FaturaNo = entity.FaturaNo,
                FaturaTipi = entity.FaturaTipi,
                CariId = entity.CariId,
                CariAdi = entity.Cari?.Unvan,
                Tarih = entity.Tarih,
                ToplamTutar = entity.ToplamTutar,
                KdvTutari = entity.KdvTutari,
                GenelToplam = entity.GenelToplam,
                Aciklama = entity.Aciklama,
                Durum = entity.Durum,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Kalemler = entity.Kalemler.Select(k => new FaturaKalemiDto
                {
                    Id = k.Id,
                    FaturaId = k.FaturaId,
                    UrunId = k.UrunId,
                    UrunAdi = k.Urun?.Adi,
                    Miktar = k.Miktar,
                    BirimFiyat = k.BirimFiyat,
                    KdvOrani = k.KdvOrani,
                    KdvTutari = k.KdvTutari,
                    Tutar = k.Tutar
                }).ToList()
            };
        }

        public async Task<List<FaturaDto>> GetAllAsync(string? faturaTipi = null)
        {
            var query = _context.Faturalar
                .Include(f => f.Cari)
                .AsQueryable();

            if (!string.IsNullOrEmpty(faturaTipi))
            {
                query = query.Where(f => f.FaturaTipi == faturaTipi);
            }

            var list = await query.ToListAsync();

            return list.Select(entity => new FaturaDto
            {
                Id = entity.Id,
                FaturaNo = entity.FaturaNo,
                FaturaTipi = entity.FaturaTipi,
                CariId = entity.CariId,
                CariAdi = entity.Cari?.Unvan,
                Tarih = entity.Tarih,
                ToplamTutar = entity.ToplamTutar,
                KdvTutari = entity.KdvTutari,
                GenelToplam = entity.GenelToplam,
                Durum = entity.Durum
            }).ToList();
        }

        public async Task UpdateAsync(int id, FaturaCreateDto dto)
        {
            var fatura = await _context.Faturalar
                .Include(f => f.Kalemler)
                .FirstOrDefaultAsync(f => f.Id == id);
            
            if (fatura == null) return;

            fatura.FaturaTipi = dto.FaturaTipi ?? fatura.FaturaTipi;
            fatura.CariId = dto.CariId ?? fatura.CariId;
            if (dto.Tarih.HasValue)
            {
                fatura.Tarih = dto.Tarih.Value.Kind == DateTimeKind.Utc 
                    ? dto.Tarih.Value 
                    : dto.Tarih.Value.ToUniversalTime();
            }
            fatura.Aciklama = dto.Aciklama ?? fatura.Aciklama;
            fatura.UpdatedAt = DateTime.UtcNow;

            // Kalemleri güncelle
            if (dto.Kalemler != null)
            {
                // Mevcut kalemleri sil
                _context.FaturaKalemleri.RemoveRange(fatura.Kalemler);
                fatura.Kalemler.Clear();

                // Yeni kalemleri ekle
                decimal toplamTutar = 0;
                decimal kdvTutari = 0;

                foreach (var kalemDto in dto.Kalemler)
                {
                    var urun = await _context.Urunler.FindAsync(kalemDto.UrunId);
                    var kdvOrani = kalemDto.KdvOrani ?? urun?.KdvOrani ?? 0;
                    var tutar = kalemDto.Miktar * kalemDto.BirimFiyat;
                    var kdv = tutar * kdvOrani / 100;

                    var kalem = new FaturaKalemi
                    {
                        FaturaId = fatura.Id,
                        UrunId = kalemDto.UrunId,
                        Miktar = kalemDto.Miktar,
                        BirimFiyat = kalemDto.BirimFiyat,
                        KdvOrani = kdvOrani,
                        KdvTutari = kdv,
                        Tutar = tutar
                    };

                    fatura.Kalemler.Add(kalem);
                    toplamTutar += tutar;
                    kdvTutari += kdv;
                }

                fatura.ToplamTutar = toplamTutar;
                fatura.KdvTutari = kdvTutari;
                fatura.GenelToplam = toplamTutar + kdvTutari;
            }

            await _context.SaveChangesAsync();
        }
    }
}

