using QueueUp.Domain.Entities;

namespace QueueUp.Domain.Interfaces;

public interface IQueueRepository
{
    public Task<Queue?> StartQueue(Guid establishmentId, Guid userId, int slots);
    public Task StopQueue(Guid establishmentId, Guid userId);
    public Task<Queue?> GetQueueById(Guid queueId);
    public Task<QueueUser> EnterQueue(Guid queueId, Guid userId);
    public Task<int> CountQueueUsers(Guid queueId);
    public Task<int> CountServicesToday(Guid establishmentId);
    public Task<QueueUser?> GetQueueUser(Guid userId, Guid establishmentId);
    public Task<QueueUser?> GetNextQueueUser(Guid queueId);
    public Task<QueueUser?> UpdateQueueUser(QueueUser queueUser);
    public Task<List<QueueUser>> GetFinishedQueueUsersByEstablishmentId(Guid establishmentId);
    public Task<int> CountUsersInQueue(Guid queueId);
    public Task<QueueUser?> GetQueueUserById(Guid userId, Guid queueId);
    public Task<QueueUser?> LeaveQueue(Guid userId, Guid queueId);
    public Task<List<QueueUser>> GetQueueUsers(Guid queueId); 
}