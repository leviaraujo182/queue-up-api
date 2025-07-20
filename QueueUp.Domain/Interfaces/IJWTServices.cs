using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IJwtServices
{
    public Task<string> GenerateToken(User user);
}