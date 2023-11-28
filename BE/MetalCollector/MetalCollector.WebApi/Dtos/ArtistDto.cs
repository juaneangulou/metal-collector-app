using System.Text.Json.Serialization;

namespace MetalCollector.WebApi.Dtos
{
    public class ArtistDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("em_id")]
        public string EmId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("genre")]
        public string Genre { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("formed")]
        public string Formed { get; set; }

        [JsonPropertyName("active")]
        public string Active { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("photo")]
        public string Photo { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("discography")]
        public List<DiscographyDto> Discography { get; set; }

        [JsonPropertyName("lineup")]
        public List<LineupDto> Lineup { get; set; }

        [JsonPropertyName("social")]
        public List<SocialDto> Social { get; set; }
    }

    public class DiscographyDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }
    }

    public class LineupDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("instrument")]
        public string Instrument { get; set; }
    }

    public class SocialDto
    {
        [JsonPropertyName("media")]
        public string Media { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
