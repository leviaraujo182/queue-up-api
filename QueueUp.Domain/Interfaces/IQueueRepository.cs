using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IQueueRepository
{
    public Task<Queue?> StartQueue(Guid establishmentId, Guid userId, int slots);
    public Task StopQueue(Guid establishmentId, Guid userId);
    public Task<Queue?> GetQueueById(Guid queueId);
    public Task<QueueUser> EnterQueue(Guid queueId, Guid userId);
    public Task<int> CountQueueUsers(Guid queueId);
    public Task<QueueUser?> GetQueueUser(Guid userId, Guid establishmentId);
}