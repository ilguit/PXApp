using Microsoft.EntityFrameworkCore;
using PXApp.Core.Db.Entity;

namespace PXApp.Core.Db;

public class PXAppDbContext : DbContext
{
    public PXAppDbContext(DbContextOptions<PXAppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");
        modelBuilder.HasPostgresExtension("uuid-ossp");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    public DbSet<TableMessage> Messages => Set<TableMessage>();
}