namespace OtomasyonApi.DTOs
{
    public class FaturaKalemiDto
    {
        public int Id { get; set; }
        public int FaturaId { get; set; }
        public int UrunId { get; set; }
        public string? UrunAdi { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal? KdvOrani { get; set; }
        public decimal? KdvTutari { get; set; }
        public decimal Tutar { get; set; }
    }

    public class FaturaKalemiCreateDto
    {
        public int UrunId { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal? KdvOrani { get; set; }
    }
}

