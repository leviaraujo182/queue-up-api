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

    public async Task<Establishment?> GetEstablishmentById(Guid establishmentId)
    {
        var establishment = await establishmentRepository.GetEstablishmentById(establishmentId);
        
        if (establishment is null)
            throw new NotFoundEstablishmentException("Não foi encontrado estabelecimento para o id informado");
        
        return establishment;
    }

    public async Task<EstablishmentRating> CreateRating(Guid userId, Guid establishmentId, int ratingValue)
    {
        var userQueue = await queueRepository.GetQueueUser(userId, establishmentId);
        
        if(userQueue is null)
            throw new NotFoundQueueUserException("Usuário não encontrado na fila deste estabelecimento");
        
        var rating = await establishmentRepository.CreateRating(userId, establishmentId, ratingValue);

        await UpdateRating(establishmentId);

        return rating;
    }

    public async Task UpdateRating(Guid establishmentId)
    {
        var establishmentRatings = await establishmentRepository.GetEstablishmentRatings(establishmentId);

        var establishmentRatingSum = establishmentRatings.Sum(r => r.Rating);
        var establishmentRatingCount = establishmentRatings.Count;

        var newRating = Math.Round(establishmentRatingSum / establishmentRatingCount, 2);
        
        if(double.IsNaN(newRating))
            newRating = 0;
        
        await establishmentRepository.UpdateRating(establishmentId, newRating);
    }
}