namespace OtomasyonApi.DTOs
{
    public class CariHareketCreateDto
    {
        public int CariId { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string? Aciklama { get; set; }
        public decimal Borc { get; set; }
        public decimal Alacak { get; set; }
        public string? EvrakTipi { get; set; }
        public int? ReferansId { get; set; }
        public int? KasaId { get; set; }
        public int? BankaId { get; set; }
        public string? Doviz { get; set; }
        public decimal? Kur { get; set; }
    }
}
