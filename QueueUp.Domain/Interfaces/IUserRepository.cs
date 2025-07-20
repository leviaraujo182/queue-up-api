using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User> CreateAsync(User user);
    public Task<User?> FindByIdAsync(Guid id);
    public Task<User?> FindByEmailAsync(string email);
}