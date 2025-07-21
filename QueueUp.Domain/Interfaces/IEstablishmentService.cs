using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IEstablishmentService
{
    public Task<Establishment> Create(Establishment establishment);
}