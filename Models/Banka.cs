using System.ComponentModel.DataAnnotations.Schema;

namespace OtomasyonApi.Models
{
    [Table("bankalar")]
    public class Banka
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("bankaadi")]
        public string BankaAdi { get; set; } = string.Empty;

        [Column("subeadi")]
        public string? SubeAdi { get; set; }

        [Column("hesapno")]
        public string? HesapNo { get; set; }

        [Column("iban")]
        public string? Iban { get; set; }

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

