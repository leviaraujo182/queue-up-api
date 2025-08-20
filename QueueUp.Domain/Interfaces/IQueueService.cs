using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IQueueService
{
    public Task<Queue?> StartQueue(Guid establishmentId, Guid userId, int slots);
    public Task StopQueue(Guid establishmentId, Guid userId);
    public Task<QueueUser> EnterQueue(Guid queueId, Guid userId);
    public Task<QueueUser?> GetQueueUser(Guid userId, Guid establishmentId);
    public Task<QueueUser?> StartNextQueueUser(Guid establishmentId);
    public Task<int> CountInQueueUsers(Guid queueId, Guid establishmentId);
    public Task<QueueUser?> GetQueueUserById(Guid userId, Guid queueId);
    public Task<QueueUser?> LeaveQueue(Guid userId, Guid queueId);
}