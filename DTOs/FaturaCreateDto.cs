namespace OtomasyonApi.DTOs
{
    public class FaturaCreateDto
    {
        public string? FaturaTipi { get; set; } // Satis veya Alis
        public int? CariId { get; set; }
        public DateTime? Tarih { get; set; }
        public string? Aciklama { get; set; }
        public List<FaturaKalemiCreateDto>? Kalemler { get; set; }
    }
}

