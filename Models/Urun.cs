using System.ComponentModel.DataAnnotations.Schema;

namespace OtomasyonApi.Models
{
    [Table("urunler")]
    public class Urun
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("urunkodu")]
        public string UrunKodu { get; set; } = string.Empty;

        [Column("adi")]
        public string Adi { get; set; } = string.Empty;

        [Column("barkod")]
        public string? Barkod { get; set; }

        [Column("birim")]
        public string? Birim { get; set; }

        [Column("birimfiyat")]
        public decimal? BirimFiyat { get; set; }

        [Column("kdvorani")]
        public decimal? KdvOrani { get; set; }

        [Column("stokmiktari")]
        public decimal? StokMiktari { get; set; }

        [Column("minstokmiktari")]
        public decimal? MinStokMiktari { get; set; }

        [Column("kategori")]
        public string? Kategori { get; set; }

        [Column("marka")]
        public string? Marka { get; set; }

        [Column("aciklama")]
        public string? Aciklama { get; set; }

        [Column("durum")]
        public bool Durum { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}

