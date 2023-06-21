
using projekt.Models;

namespace Projekt.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {
           
        }
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Konto> Konta{ get; set; }
        public DbSet<Dania> Dania { get; set; }
        public DbSet<Jadlospis> Jadlospisy { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Konto>()
                .HasMany(p => p.Jadlospisy)
                .WithOne(pc => pc.Konta)
                .HasForeignKey(pc => pc.IdKonta);

            modelBuilder.Entity<Dania>()
                .HasMany(c => c.Jadlospisy)
                .WithOne(pc => pc.Dania)
                .HasForeignKey(pc => pc.IdDania);

            modelBuilder.Entity<Jadlospis>()
                .HasKey(pc => new { pc.IdDania, pc.IdKonta });

            modelBuilder.Entity<Produkt>()
                .HasMany(p => p.DaniaProdukty)
                .WithOne(pc => pc.Produkty)
                .HasForeignKey(pc => pc.IdProduktu);

            modelBuilder.Entity<Dania>()
                .HasMany(c => c.DaniaProduktiy)
                .WithOne(pc => pc.Dania)
                .HasForeignKey(pc => pc.IdDania);

            modelBuilder.Entity<DaniaProdukty>()
                .HasKey(pc => new { pc.IdProduktu, pc.Dania });
        }

    }
}