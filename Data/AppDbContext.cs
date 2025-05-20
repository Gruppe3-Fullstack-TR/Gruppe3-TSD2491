using Gruppe3.Models;
using Microsoft.EntityFrameworkCore;

namespace Gruppe3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PollenRegistering> PollenRegisterings { get; set; }
        public DbSet<PollenResponse> PollenResponses { get; set; }
        public DbSet<DateInfo> DateInfos { get; set; }
        public DbSet<IndexInfo> IndexInfos { get; set; }
        public DbSet<ColorInfo> ColorInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurer relasjoner mellom tabeller her om nødvendig
            modelBuilder.Entity<PollenResponse>()
                .HasOne(pr => pr.DateInfo)
                .WithMany()
                .HasForeignKey(pr => pr.DateInfoId);

            // Fjernet feil relasjon til IndexInfo fra PollenResponse

            modelBuilder.Entity<IndexInfo>()
                .HasOne(ii => ii.ColorInfo)
                .WithMany()
                .HasForeignKey(ii => ii.ColorInfoId);
        }
    }
}
