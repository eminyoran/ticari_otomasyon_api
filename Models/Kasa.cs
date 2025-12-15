using System.ComponentModel.DataAnnotations.Schema;

namespace OtomasyonApi.Models
{
    [Table("kasalar")]
    public class Kasa
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("kasaadi")]
        public string KasaAdi { get; set; } = string.Empty;

        [Column("kasakodu")]
        public string? KasaKodu { get; set; }

        [Column("bakiye")]
        public decimal? Bakiye { get; set; }

        [Column("parabirimi")]
        public string? ParaBirimi { get; set; }

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

