namespace OtomasyonApi.DTOs
{
    public class UrunDto
    {
        public int Id { get; set; }
        public string UrunKodu { get; set; } = string.Empty;
        public string Adi { get; set; } = string.Empty;
        public string? Barkod { get; set; }
        public string? Birim { get; set; }
        public decimal? BirimFiyat { get; set; }
        public decimal? KdvOrani { get; set; }
        public decimal? StokMiktari { get; set; }
        public decimal? MinStokMiktari { get; set; }
        public string? Kategori { get; set; }
        public string? Marka { get; set; }
        public string? Aciklama { get; set; }
        public bool Durum { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

