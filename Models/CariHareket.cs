using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtomasyonApi.Models
{
    [Table("carihareketler")]
    public class CariHareket
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("cariid")]
        public int CariId { get; set; }

        [Column("tarih")]
        public DateTime Tarih { get; set; }

        [Column("aciklama")]
        public string? Aciklama { get; set; }

        [Column("borc")]
        public decimal Borc { get; set; }

        [Column("alacak")]
        public decimal Alacak { get; set; }

        [Column("evraktipi")]
        public string? EvrakTipi { get; set; }

        [Column("referansid")]
        public int? ReferansId { get; set; }

        [Column("kasaid")]
        public int? KasaId { get; set; }

        [Column("bankaid")]
        public int? BankaId { get; set; }

        [Column("doviz")]
        public string? Doviz { get; set; }

        [Column("kur")]
        public decimal? Kur { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Cari Cari { get; set; } = null!;
    }
}
