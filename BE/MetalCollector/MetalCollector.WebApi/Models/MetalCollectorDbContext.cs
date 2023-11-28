using Microsoft.EntityFrameworkCore;

namespace MetalCollector.WebApi.Models
{
    public class MetalCollectorDbContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=api-metalcollector.mymetalevents.com;Database=mymetalevents_metalcollectordb;User Id=metalcollector_admin;Password=Icaro88.21;Encrypt=False;TrustServerCertificate=True;");
        }

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
        }
    }
}