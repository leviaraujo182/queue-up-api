using QueueUp.Application.Exceptions.Establishment;
using QueueUp.Application.Exceptions.Queue;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.Application.Services;

public class EstablishmentService(IEstablishmentRepository establishmentRepository, IQueueRepository queueRepository) : IEstablishmentService
{
    public async Task<Establishment> Create(Establishment establishment)
    {
        var createdEstablishment = await establishmentRepository.Create(establishment);

        return createdEstablishment;
    }

    public async Task<List<Establishment>?> GetEstablishmentsByOwnerId(Guid ownerId, EstablishmentFilters establishmentFilters)
    {
        return await establishmentRepository.GetEstablishmentsByOwnerId(ownerId, establishmentFilters);
    }

    public Task<EstablishmentsMetrics> GetEstablishmentsMetrics(Guid ownerId)
    {
        return establishmentRepository.GetEstablishmentsMetrics(ownerId);
    }

    public Task<Establishment?> GetEstablishmentById(Guid establishmentId)
    {
        return establishmentRepository.GetEstablishmentById(establishmentId);
    }

    public async Task<EstablishmentRating> CreateRating(Guid userId, Guid establishmentId, int ratingValue)
    {
        var userQueue = await queueRepository.GetQueueUser(userId, establishmentId);
        
        if(userQueue is null)
            throw new NotFoundQueueUserException("Usuário não encontrado na fila deste estabelecimento");
        
        return await establishmentRepository.CreateRating(userId, establishmentId, ratingValue);
    }
}