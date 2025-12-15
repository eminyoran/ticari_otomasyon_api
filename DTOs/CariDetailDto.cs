namespace OtomasyonApi.DTOs
{
    public class CariDetailDto
    {
        public int Id { get; set; }
        public string CariKodu { get; set; }
        public string Unvan { get; set; }
        public string? TicariUnvan { get; set; }
        public string? VergiDairesi { get; set; }
        public string? VergiNo { get; set; }
        public string? TcNo { get; set; }
        public string? Telefon { get; set; }
        public string? Telefon2 { get; set; }
        public string? Email { get; set; }
        public string? Adres { get; set; }
        public string? Ulke { get; set; }
        public string? Sehir { get; set; }
        public string? Ilce { get; set; }
        public string? Iban { get; set; }
        public string? CariTipi { get; set; }
        public string? ParaBirimi { get; set; }
        public bool Durum { get; set; }
        public decimal Bakiye { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

