using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;
using QueueUp.Infraestructure.Context;

namespace QueueUp.Infraestructure.Repositories;

public class EstablishmentRepository(AppDbContext appDbContext) : IEstablishmentRepository
{
    public async Task<Establishment> Create(Establishment establishment)
    {
        var createdEstablishment = await appDbContext.Establishments.AddAsync(establishment);
        var entity = createdEstablishment.Entity;
        await appDbContext.SaveChangesAsync();

        return entity;
    }
}