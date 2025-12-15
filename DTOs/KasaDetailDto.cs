namespace OtomasyonApi.DTOs
{
    public class KasaDetailDto
    {
        public int Id { get; set; }
        public string KasaAdi { get; set; } = string.Empty;
        public string? KasaKodu { get; set; }
        public decimal? Bakiye { get; set; }
        public string? ParaBirimi { get; set; }
        public string? Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

