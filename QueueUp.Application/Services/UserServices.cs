using QueueUp.Application.Exceptions.User;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.Application.Services;

public class UserServices(IUserRepository userRepository) : IUserServices
{
    public async Task<User> CreateAsync(User user)
    {
        var userWithMail = await userRepository.FindByEmailAsync(user.Email);

        if (userWithMail != null)
            throw new EmailNotAvailableException("Já existe um usuário com este e-mail cadastrado");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        user.Password = hashedPassword;

        var createdUser = await userRepository.CreateAsync(user);

        return createdUser;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var user = await userRepository.FindByEmailAsync(email);

        return user;
    }

    public async Task<User?> FindByIdAsync(Guid id)
    {
        var user = await userRepository.FindByIdAsync(id);

        return user;
    }
}