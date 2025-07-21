using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.Application.Services;

public class EstablishmentService(IEstablishmentService establishmentService) : IEstablishmentService
{
    public async Task<Establishment> Create(Establishment establishment)
    {
        var createdEstablishment = await establishmentService.Create(establishment);

        return createdEstablishment;
    }
}