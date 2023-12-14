namespace MetalCollector.WebApi.Models
{
    public class Artist
    {
        public string Id { get; set; }  // Asumiendo que es un int
        public string EmId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Location { get; set; }
        public string Formed { get; set; }  // Considera usar DateTime si almacenas fechas
        public string Active { get; set; }
        public string Label { get; set; }
        public string Photo { get; set; }
        public string Link { get; set; }
        public string Status { get; set; }
        public string? MAArtistId { get; set; }


        public ICollection<Discography> Discographies { get; set; }
        public ICollection<Lineup> Lineup { get; set; }
        public ICollection<Social> Social { get; set; }
        public ICollection<Item> Items { get; set; }

    }
    public class Discography
    {
        public string Id { get; set; }  // Asumiendo que es un int
        public string Title { get; set; }
        public string Type { get; set; }
        public string Year { get; set; }  // Considera usar int o DateTime

        public string ArtistId { get; set; }
        public string? MADiscographyId { get; set; }
        public Artist Artist { get; set; }
    }
    public class Lineup
    {
        public string Id { get; set; }  // Asumiendo que es un int
        public string Name { get; set; }
        public string Instrument { get; set; }
        public string ArtistId { get; set; }
        public string? MALineupId { get; set; }
        public Artist Artist { get; set; }
    }
    public class Social
    {
        public string Id { get; set; }  // Asumiendo que es un int
        public string Media { get; set; }
        public string Url { get; set; }

        public string ArtistId { get; set; }
        public string? MASocialId { get; set; }
        public Artist Artist { get; set; }
    }

}
