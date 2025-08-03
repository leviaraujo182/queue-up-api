using Microsoft.EntityFrameworkCore;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;
using QueueUp.Infraestructure.Context;

namespace QueueUp.Infraestructure.Repositories;

public class QueueRepository(AppDbContext appDbContext) : IQueueRepository
{
    public async Task<Queue?> StartQueue(Guid establishmentId, Guid userId, int slots)
    {

        var queue = new Queue
        {
            Id = Guid.NewGuid(),
            Slots = slots,
            EstablishmentId = establishmentId,
            CreatedAt = DateTime.UtcNow
        };
        
        var entry = await appDbContext.Queues.AddAsync(queue);
        var queueEntity = entry.Entity;

        await appDbContext.SaveChangesAsync();

        return queueEntity;
    }

    public async Task StopQueue(Guid establishmentId, Guid userId)
    {
        var establishment = await appDbContext.Establishments.FirstOrDefaultAsync(x => x.Id == establishmentId && x.UserId == userId);

        if (establishment != null)
        {
            establishment.QueueId = null;
        
            await appDbContext.SaveChangesAsync();
        }
    }

    public async Task<Queue?> GetQueueById(Guid queueId)
    {
        var queue = await appDbContext.Queues.FirstOrDefaultAsync(x => x.Id == queueId);

        return queue;
    }

    public async Task<QueueUser> EnterQueue(Guid queueId, Guid userId)
    {
        var findedQueueUser = await appDbContext.QueueUsers
            .Where(x => x.QueueId == queueId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
        
        var position = 0;

        if (findedQueueUser != null)
            position = findedQueueUser.Position + 1;
        
        var queueuser = new QueueUser
        {
            Id = Guid.NewGuid(),
            Position = position,
            QueueId = queueId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        var entryQueueUser = await appDbContext.QueueUsers.AddAsync(queueuser);
        
        var queueUserEntity = entryQueueUser.Entity;

        await appDbContext.SaveChangesAsync();

        return queueUserEntity;
    }

    public Task<int> CountQueueUsers(Guid queueId)
    {
        var count = appDbContext.QueueUsers
            .Where(x => x.QueueId == queueId)
            .CountAsync();

        return count;
    }

    public async Task<QueueUser?> GetQueueUser(Guid userId, Guid establishmentId)
    {
       return await appDbContext.QueueUsers
            .Include(x => x.Queue)
            .Where(x => x.UserId == userId && x.Queue.EstablishmentId == establishmentId)
            .FirstOrDefaultAsync();
    }
}