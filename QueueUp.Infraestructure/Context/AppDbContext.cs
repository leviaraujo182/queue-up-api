using Microsoft.EntityFrameworkCore;
using QueueUp.Domain.Entities;

namespace QueueUp.Infraestructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Establishment> Establishments { get; set; }
    public DbSet<EstablishmentAddress> EstablishmentAddresses { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Queue> Queues { get; set; }
    public DbSet<QueueUser> QueueUsers { get; set; }
    public DbSet<EstablishmentRating> EstablishmentRatings { get; set; }

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
            .HasForeignKey<User>(x => x.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Establishment>()
            .HasOne(x => x.EstablishmentAddress)
            .WithOne()
            .HasForeignKey<Establishment>(x => x.AddressId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Establishment>()
            .HasMany(x => x.EstablishmentRatings)
            .WithOne(x => x.Establishment)
            .HasForeignKey(x => x.EstablishmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EstablishmentRating>()
            .HasOne(x => x.User)
            .WithMany(x => x.EstablishmentRatings)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Establishment>()
            .HasOne(x => x.Queue)
            .WithOne(x => x.Establishment)
            .IsRequired(false)
            .HasForeignKey<Establishment>(x => x.QueueId);

        modelBuilder.Entity<Queue>()
            .HasMany(x => x.QueueUsers)
            .WithOne(x => x.Queue)
            .HasForeignKey(x => x.QueueId);
    }
}