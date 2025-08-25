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
        
        var establishment = await appDbContext.Establishments.FirstOrDefaultAsync(x => x.Id == establishmentId && x.UserId == userId);

        if (establishment != null)
            establishment.QueueId = queueEntity.Id;
        
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
            .Where(x => x.QueueId == queueId && x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
        
        var position = 1;

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
            .Where(x => x.QueueId == queueId && x.DeletedAt == null)
            .CountAsync();

        return count;
    }

    public Task<int> CountServicesToday(Guid queueId)
    {
        return appDbContext.QueueUsers.Where(x => x.QueueId == queueId && 
                                                  x.StartDate != null && 
                                                  x.EndDate != null && 
                                                  x.CreatedAt.Date == DateTime.UtcNow.Date)
            .CountAsync();
    }

    public async Task<QueueUser?> GetQueueUser(Guid userId, Guid establishmentId)
    {
       return await appDbContext.QueueUsers
            .Include(x => x.Queue)
            .Where(x => x.UserId == userId && x.Queue.EstablishmentId == establishmentId)
            .FirstOrDefaultAsync();
    }
    
    public async Task<QueueUser?> GetNextQueueUser(Guid queueId)
    {
        var queueUser = await appDbContext.QueueUsers
            .Where(x => x.QueueId == queueId)
            .OrderBy(x => x.Position)
            .FirstOrDefaultAsync();

        return queueUser;
    }

    public async Task<QueueUser?> UpdateQueueUser(QueueUser queueUser)
    {
        appDbContext.QueueUsers.Update(queueUser);
        await appDbContext.SaveChangesAsync();

        return queueUser;
    }

    public async Task<List<QueueUser>> GetFinishedQueueUsersByEstablishmentId(Guid establishmentId)
    {
       var queueUsers = await appDbContext.QueueUsers
            .Where(x => x.Queue.EstablishmentId == establishmentId && x.StartDate != null && x.EndDate != null)
            .ToListAsync();

        return queueUsers;
    }

    public async Task<int> CountUsersInQueue(Guid queueId)
    {
        return await appDbContext.QueueUsers.CountAsync(x => x.QueueId == queueId && 
                                                             x.StartDate == null && 
                                                             x.EndDate == null && 
                                                             x.DeletedAt == null);
    }

    public async Task<QueueUser?> GetQueueUserById(Guid userId, Guid queueId)
    {
        return await appDbContext.QueueUsers
            .Where(x => x.UserId == userId && x.QueueId == queueId && x.DeletedAt == null)
            .FirstOrDefaultAsync();
    }

    public async Task<QueueUser?> LeaveQueue(Guid userId, Guid queueId)
    {
        var queueUser = await appDbContext.QueueUsers.FirstOrDefaultAsync(x => x.UserId == userId && x.QueueId == queueId && x.DeletedAt == null);

        if (queueUser == null) return queueUser;
        
        queueUser.DeletedAt = DateTime.UtcNow;

        await appDbContext.SaveChangesAsync();

        return queueUser;
    }

    public async Task<List<QueueUser>> GetQueueUsers(Guid queueId)
    {
        return await appDbContext.QueueUsers
            .Where(x => x.QueueId == queueId && x.DeletedAt == null)
            .AsNoTracking()
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }
}