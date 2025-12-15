namespace OtomasyonApi.DTOs
{
    public class KasaDto
    {
        public int Id { get; set; }
        public string KasaAdi { get; set; } = string.Empty;
        public string? KasaKodu { get; set; }
        public decimal? Bakiye { get; set; }
        public string? ParaBirimi { get; set; }
        public bool Durum { get; set; }
    }
}

