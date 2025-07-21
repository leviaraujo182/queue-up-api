using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IEstablishmentRepository
{
    public Task<Establishment> Create(Establishment establishment);
}