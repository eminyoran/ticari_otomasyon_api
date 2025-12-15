namespace OtomasyonApi.DTOs
{
    public class BankaDto
    {
        public int Id { get; set; }
        public string BankaAdi { get; set; } = string.Empty;
        public string? SubeAdi { get; set; }
        public string? HesapNo { get; set; }
        public string? Iban { get; set; }
        public decimal? Bakiye { get; set; }
        public string? ParaBirimi { get; set; }
        public bool Durum { get; set; }
    }
}

