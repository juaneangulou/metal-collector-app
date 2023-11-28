namespace MetalCollector.WebApi.Models
{
    public class Band
    {
        public string BandId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Genre { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Formed { get; set; }
        public string Label { get; set; }
        public string Location { get; set; }
        public ICollection<Member> Lineup { get; set; }
        public ICollection<Release> Discography { get; set; }
        public string Link { get; set; }
        public string Photo { get; set; }

        public Band()
        {
            BandId = Guid.NewGuid().ToString();
            Lineup = new List<Member>();
            Discography = new List<Release>();
        }
    }

    public class Member
    {
        public string MemberId { get; set; }
        public string BandId { get; set; }
        public string Name { get; set; }
        public string Instrument { get; set; }

        public Member()
        {
            MemberId = Guid.NewGuid().ToString();
        }
    }

    public class Release
    {
        public string ReleaseId { get; set; }
        public string BandId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Year { get; set; }

        public Release()
        {
            ReleaseId = Guid.NewGuid().ToString();
        }
    }

    public class Item
    {
        public string ItemId { get; set; }
        public string ArtistId { get; set; }
        public string Barcode { get; set; }
        public DateTime? BuyDate { get; set; }
        public string EmId { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }

        public Item()
        {
            ItemId = Guid.NewGuid().ToString();
        }
    }
}
