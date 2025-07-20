using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IAuthService
{
    public Task<string> LoginAsync(Login login);
    public Task RecoveryPasswordAsync();
    public Task ChangePasswordAsync();
}