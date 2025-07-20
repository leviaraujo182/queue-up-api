using Microsoft.Extensions.DependencyInjection;
using QueueUp.Application.Services;
using QueueUp.Domain.Interfaces;

namespace QueueUp.Application.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServicesExtensions(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IAuthService, AuthServices>();
        services.AddScoped<IJwtServices, JwtServices>();
        
        return services;
    }
}