using Microsoft.Extensions.DependencyInjection;
using QueueUp.Domain.Interfaces;
using QueueUp.Infraestructure.Repositories;

namespace QueueUp.Application.Extensions;

public static class RepositoryServicesExtensions
{
    public static IServiceCollection AddRepositoryServicesExtensions(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        services.AddScoped<IQueueRepository, QueueRepository>();
        return services;
    }
}