using Microsoft.EntityFrameworkCore;
using QueueUp.Domain.Entities;

namespace QueueUp.Infraestructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Establishment> Establishments { get; set; }
    public DbSet<EstablishmentAddress> EstablishmentAddresses { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(p => p.Establishments)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<User>()
            .HasOne(x => x.Address)
            .WithOne(x => x.User)
            .HasForeignKey<User>(x => x.AddressId);

        modelBuilder.Entity<Establishment>()
            .HasOne(x => x.EstablishmentAddress)
            .WithOne(x => x.Establishment)
            .HasForeignKey<Establishment>(x => x.AddressId);
    }
}