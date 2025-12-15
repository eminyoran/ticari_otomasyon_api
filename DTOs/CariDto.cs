namespace OtomasyonApi.DTOs
{
    public class CariDto
    {
        public int Id { get; set; }
        public string CariKodu { get; set; }
        public string Unvan { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string? Sehir { get; set; }
        public string? Ilce { get; set; }
        public decimal Bakiye { get; set; }
    }
}
