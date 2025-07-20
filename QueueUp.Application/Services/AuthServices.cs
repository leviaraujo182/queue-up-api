using QueueUp.Application.Exceptions.Auth;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.Application.Services;

public class AuthServices(IJwtServices jwtServices, IUserRepository userRepository) : IAuthService
{
    public async Task<string> LoginAsync(Login login)
    {
        var user = await userRepository.FindByEmailAsync(login.Email);

        if (user is null)
            throw new InvalidEmailOrPasswordException("Usuário ou senha inválido");

        var token = await jwtServices.GenerateToken(user);

        return token;
    }

    public Task RecoveryPasswordAsync()
    {
        throw new NotImplementedException();
    }

    public Task ChangePasswordAsync()
    {
        throw new NotImplementedException();
    }
}