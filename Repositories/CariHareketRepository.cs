using Dapper;
using OtomasyonApi.Data;
using OtomasyonApi.Models;

namespace OtomasyonApi.Repositories
{
    public class CariHareketRepository : ICariHareketRepository
    {
        private readonly DapperContext _context;

        public CariHareketRepository(DapperContext context)
        {
            _context = context;
        }

        // ------------------------------
        // 1) Cari ID’ye göre liste
        // ------------------------------
        public async Task<IEnumerable<CariHareket>> GetByCariIdAsync(int cariId)
        {
            var sql = @"SELECT * FROM carihareketler 
                        WHERE cariid = @cariId 
                        ORDER BY tarih DESC";

            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<CariHareket>(sql, new { cariId });
        }

        // ------------------------------
        // 2) ID’ye göre tek hareket
        // ------------------------------
        public async Task<CariHareket?> GetByIdAsync(int id)
        {
            var sql = @"SELECT * FROM carihareketler WHERE id = @id";

            using var conn = _context.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<CariHareket>(sql, new { id });
        }

        // ------------------------------
        // 3) Gelişmiş listeleme (filtreler)
        // ------------------------------
        public async Task<IEnumerable<CariHareket>> GetListAsync(int? cariId, DateTime? baslangic, DateTime? bitis)
        {
            var sql = @"SELECT * FROM carihareketler WHERE 1=1";

            if (cariId != null) sql += " AND cariid = @cariId";
            if (baslangic != null) sql += " AND tarih >= @baslangic";
            if (bitis != null) sql += " AND tarih <= @bitis";

            sql += " ORDER BY tarih DESC";

            using var conn = _context.CreateConnection();
            return await conn.QueryAsync<CariHareket>(sql, new { cariId, baslangic, bitis });
        }

        // ------------------------------
        // 4) Yeni hareket oluştur
        // ------------------------------
        public async Task<int> CreateAsync(CariHareket hareket)
        {
            var sql = @"
                INSERT INTO carihareketler 
                (cariid, tarih, aciklama, borc, alacak, evraktipi, referansid, kasaid, bankaid, doviz, kur, createdat)
                VALUES 
                (@CariId, @Tarih, @Aciklama, @Borc, @Alacak, @EvrakTipi, @ReferansId, @KasaId, @BankaId, @Doviz, @Kur, @CreatedAt)
                RETURNING id;
            ";

            using var conn = _context.CreateConnection();
            return await conn.ExecuteScalarAsync<int>(sql, hareket);
        }

        // ------------------------------
        // 5) Sil
        // ------------------------------
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM carihareketler WHERE id = @id";

            using var conn = _context.CreateConnection();
            var affected = await conn.ExecuteAsync(sql, new { id });

            return affected > 0;
        }
    }
}
