using EinnahmeAusgabe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EinnahmeAusgabe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Transaktion> Transaktionen { get; set; }
        public DbSet<Kategorie> Kategorien {  get; set; }
    }
}
