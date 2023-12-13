namespace MetalCollector.WebApi.Dtos
{
    public class ItemDto
    {
        public string? ItemId { get; set; }
        public string? ArtistId { get; set; }
        public string? Barcode { get; set; }
        public DateTime? BuyDate { get; set; }
        public string? EmId { get; set; }
        public string? ItemType { get; set; }
        public string Name { get; set; }
        public string? MAArtistId { get; set; }
        public ArtistMADto? Artists { get; set; }
    }
}
