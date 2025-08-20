using Microsoft.EntityFrameworkCore;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;
using QueueUp.Infraestructure.Context;

namespace QueueUp.Infraestructure.Repositories;

public class EstablishmentRepository(AppDbContext appDbContext) : IEstablishmentRepository
{
    public async Task<Establishment> Create(Establishment establishment)
    {
        await appDbContext.EstablishmentAddresses.AddAsync(establishment.EstablishmentAddress);
        await appDbContext.SaveChangesAsync();

        establishment.AddressId = establishment.EstablishmentAddress.Id;
        
        var createdEstablishment = await appDbContext.Establishments.AddAsync(establishment);
        await appDbContext.SaveChangesAsync();

        return createdEstablishment.Entity;
    }

    public async Task<List<Establishment>?> GetEstablishmentsByOwnerId(Guid ownerId, EstablishmentFilters establishmentFilters)
    {
        var list = await appDbContext.Establishments.Where(x => x.UserId == ownerId)
            .Take(establishmentFilters.PageSize)
            .Skip(establishmentFilters.PageSize * (establishmentFilters.PageNumber - 1))
            .Include(x => x.EstablishmentAddress)
            .ToListAsync();;

        return list;
    }

    public async Task<Establishment?> GetEstablishmentById(Guid establishmentId)
    {
        var establishment = await appDbContext.Establishments
            .Include(x => x.EstablishmentAddress)
            .FirstOrDefaultAsync(x => x.Id == establishmentId);

        return establishment;
    }

    public async Task<EstablishmentRating> CreateRating(Guid userId, Guid establishmentId, int ratingValue)
    {
        var establishmentRating = new EstablishmentRating
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            EstablishmentId = establishmentId,
            Rating = ratingValue,
            CreatedAt = DateTime.UtcNow
        };
        
        var rating = await appDbContext.EstablishmentRatings.AddAsync(establishmentRating);
        await appDbContext.SaveChangesAsync();
        
        return rating.Entity;
    }

    public async Task<List<EstablishmentRating>> GetEstablishmentRatings(Guid establishmentId)
    {
        var establishmentsRatings = await appDbContext.EstablishmentRatings
            .Where(x => x.EstablishmentId == establishmentId)
            .ToListAsync();

        return establishmentsRatings;
    }
    
    public async Task UpdateRating(Guid establishmentId, double newRating)
    {
        var establishment = await appDbContext.Establishments.FirstOrDefaultAsync(x => x.Id == establishmentId);

        if (establishment != null)
        {
            establishment.Rating = newRating;
            await appDbContext.SaveChangesAsync();
        }
    }

    public async Task<EstablishmentsMetrics> GetEstablishmentsMetrics(Guid ownerId)
    {
        var activeEstablishmentsCount = await appDbContext.Establishments
            .CountAsync(x => x.UserId == ownerId && x.IsActive);
        var inactiveEstablishmentsCount = await appDbContext.Establishments
            .CountAsync(x => x.UserId == ownerId && !x.IsActive);
        var totalEstablishments = await appDbContext.Establishments
            .CountAsync(x => x.UserId == ownerId);

        return new EstablishmentsMetrics
        {
            TotalActiveEstablishments = activeEstablishmentsCount,
            TotalInactiveEstablishments = inactiveEstablishmentsCount,
            TotalEstablishments = totalEstablishments
        };
    }
    
    public async Task UpdateAverageTimeEstablishment(Guid establishmentId, int averageTime)
    {
        var establishment = await appDbContext.Establishments.FirstOrDefaultAsync(x => x.Id == establishmentId);

        if (establishment != null)
        {
            establishment.AverageTime = averageTime;
            await appDbContext.SaveChangesAsync();
        }
    }
}