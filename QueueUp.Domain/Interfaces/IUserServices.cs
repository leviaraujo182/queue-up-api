using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IUserServices
{
    public Task<User> CreateAsync(User user);
    public Task<User?> FindByEmailAsync(string email);
    public Task<User?> FindByIdAsync(Guid id);
}