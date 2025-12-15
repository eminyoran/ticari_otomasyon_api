namespace OtomasyonApi.DTOs
{
    public class UrunCreateDto
    {
        public string? Adi { get; set; }
        public string? Barkod { get; set; }
        public string? Birim { get; set; }
        public decimal? BirimFiyat { get; set; }
        public decimal? KdvOrani { get; set; }
        public decimal? StokMiktari { get; set; }
        public decimal? MinStokMiktari { get; set; }
        public string? Kategori { get; set; }
        public string? Marka { get; set; }
        public string? Aciklama { get; set; }
    }
}

