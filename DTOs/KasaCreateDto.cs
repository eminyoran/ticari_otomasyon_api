namespace OtomasyonApi.DTOs
{
    public class KasaCreateDto
    {
        public string? KasaAdi { get; set; }
        public string? KasaKodu { get; set; }
        public decimal? Bakiye { get; set; }
        public string? ParaBirimi { get; set; }
        public string? Aciklama { get; set; }
    }
}

