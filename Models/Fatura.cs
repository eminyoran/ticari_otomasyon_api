using System.ComponentModel.DataAnnotations.Schema;

namespace OtomasyonApi.Models
{
    [Table("faturalar")]
    public class Fatura
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("faturano")]
        public string FaturaNo { get; set; } = string.Empty;

        [Column("faturatipi")]
        public string FaturaTipi { get; set; } = "Satis"; // Satis veya Alis

        [Column("cariid")]
        public int? CariId { get; set; }

        [Column("tarih")]
        public DateTime Tarih { get; set; }

        [Column("toplamtutar")]
        public decimal? ToplamTutar { get; set; }

        [Column("kdvtutari")]
        public decimal? KdvTutari { get; set; }

        [Column("geneltoplam")]
        public decimal? GenelToplam { get; set; }

        [Column("aciklama")]
        public string? Aciklama { get; set; }

        [Column("durum")]
        public bool Durum { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public Cari? Cari { get; set; }
        public List<FaturaKalemi> Kalemler { get; set; } = new();
    }

    [Table("faturakalemleri")]
    public class FaturaKalemi
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("faturaid")]
        public int FaturaId { get; set; }

        [Column("urunid")]
        public int UrunId { get; set; }

        [Column("miktar")]
        public decimal Miktar { get; set; }

        [Column("birimfiyat")]
        public decimal BirimFiyat { get; set; }

        [Column("kdvorani")]
        public decimal? KdvOrani { get; set; }

        [Column("kdvtutari")]
        public decimal? KdvTutari { get; set; }

        [Column("tutar")]
        public decimal Tutar { get; set; }

        // Navigation
        public Fatura Fatura { get; set; } = null!;
        public Urun? Urun { get; set; }
    }
}

