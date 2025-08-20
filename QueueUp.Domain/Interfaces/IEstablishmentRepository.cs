using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IEstablishmentRepository
{
    public Task<Establishment> Create(Establishment establishment);
    public Task<List<Establishment>?> GetEstablishmentsByOwnerId(Guid ownerId, EstablishmentFilters establishmentFilters);
    public Task<EstablishmentsMetrics> GetEstablishmentsMetrics(Guid ownerId);
    public Task<Establishment?> GetEstablishmentById(Guid establishmentId);
    public Task<EstablishmentRating> CreateRating(Guid userId, Guid establishmentId, int ratingValue);
    public Task<List<EstablishmentRating>> GetEstablishmentRatings(Guid establishmentId);
    public Task UpdateRating(Guid establishmentId, double newRating);
    public Task UpdateAverageTimeEstablishment(Guid establishmentId, int averageTime);
}