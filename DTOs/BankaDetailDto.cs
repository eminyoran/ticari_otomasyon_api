namespace OtomasyonApi.DTOs
{
    public class BankaDetailDto
    {
        public int Id { get; set; }
        public string BankaAdi { get; set; } = string.Empty;
        public string? SubeAdi { get; set; }
        public string? HesapNo { get; set; }
        public string? Iban { get; set; }
        public decimal? Bakiye { get; set; }
        public string? ParaBirimi { get; set; }
        public string? Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

