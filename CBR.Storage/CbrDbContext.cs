using CBR.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CBR.Storage;

public class CbrDbContext : DbContext
{
    public CbrDbContext(DbContextOptions<CbrDbContext> options) : base(options)
    {
    }

    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyCourse> CurrencyCourses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // compound pk with two columns
        modelBuilder.Entity<CurrencyCourse>()
            .HasKey(c => new { c.CurrencyId, c.Date });

        modelBuilder.Entity<CurrencyCourse>()
            .HasIndex(c => new { c.CurrencyId, c.Date });

        base.OnModelCreating(modelBuilder);
    }
}

