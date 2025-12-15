using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtomasyonApi.Models
{
    [Table("cariler")]
    public class Cari
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("carikodu")]
        public string CariKodu { get; set; } = string.Empty;

        [Column("unvan")]
        public string Unvan { get; set; } = string.Empty;

        [Column("ticariunvan")]
        public string? TicariUnvan { get; set; }

        [Column("vergidairesi")]
        public string? VergiDairesi { get; set; }

        [Column("vergino")]
        public string? VergiNo { get; set; }

        [Column("tcno")]
        public string? TcNo { get; set; }

        [Column("telefon")]
        public string? Telefon { get; set; }

        [Column("telefon2")]
        public string? Telefon2 { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("adres")]
        public string? Adres { get; set; }

        [Column("ulke")]
        public string? Ulke { get; set; }

        [Column("sehir")]
        public string? Sehir { get; set; }

        [Column("ilce")]
        public string? Ilce { get; set; }

        [Column("iban")]
        public string? Iban { get; set; }

        [Column("caritipi")]
        public string? CariTipi { get; set; }

        [Column("parabirimi")]
        public string? ParaBirimi { get; set; }

        [Column("durum")]
        public bool Durum { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public List<CariHareket> Hareketler { get; set; } = new();
    }
}
