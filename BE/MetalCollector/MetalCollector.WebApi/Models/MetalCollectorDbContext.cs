using Microsoft.EntityFrameworkCore;

namespace MetalCollector.WebApi.Models
{
    public class MetalCollectorDbContext : DbContext
    {
        public MetalCollectorDbContext(DbContextOptions<MetalCollectorDbContext> options) : base(options)
        {
        }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Discography> Discographies { get; set; }
        public DbSet<Lineup> Lineups { get; set; }
        public DbSet<Social> Socials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Band>()
                .HasMany(b => b.Lineup)
                .WithOne()
                .HasForeignKey(m => m.BandId);

            modelBuilder.Entity<Band>()
                .HasMany(b => b.Discography)
                .WithOne()
                .HasForeignKey(r => r.BandId);

            modelBuilder.Entity<Artist>()
       .HasMany(a => a.Discographies)
       .WithOne()
       .HasForeignKey(d => d.ArtistId);

            modelBuilder.Entity<Discography>()
        .HasOne(d => d.Artist)
        .WithMany(a => a.Discographies)
        .HasForeignKey(d => d.ArtistId)
        .OnDelete(DeleteBehavior.Restrict);
        }

    }
}