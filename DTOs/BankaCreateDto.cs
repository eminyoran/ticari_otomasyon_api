namespace OtomasyonApi.DTOs
{
    public class BankaCreateDto
    {
        public string? BankaAdi { get; set; }
        public string? SubeAdi { get; set; }
        public string? HesapNo { get; set; }
        public string? Iban { get; set; }
        public decimal? Bakiye { get; set; }
        public string? ParaBirimi { get; set; }
        public string? Aciklama { get; set; }
    }
}

