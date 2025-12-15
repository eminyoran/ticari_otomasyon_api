namespace OtomasyonApi.DTOs
{
    public class FaturaDto
    {
        public int Id { get; set; }
        public string FaturaNo { get; set; } = string.Empty;
        public string FaturaTipi { get; set; } = string.Empty;
        public int? CariId { get; set; }
        public string? CariAdi { get; set; }
        public DateTime Tarih { get; set; }
        public decimal? ToplamTutar { get; set; }
        public decimal? KdvTutari { get; set; }
        public decimal? GenelToplam { get; set; }
        public bool Durum { get; set; }
    }
}

