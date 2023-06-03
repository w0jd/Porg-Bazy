
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
    }
}