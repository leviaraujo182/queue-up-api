using Microsoft.AspNetCore.SignalR;
using QueueUp.Application.Exceptions.Establishment;
using QueueUp.Application.Exceptions.Queue;
using QueueUp.Application.Hubs;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.Application.Services;

public class QueueService(IQueueRepository queueRepository, IEstablishmentRepository establishmentRepository, IHubContext<QueueHub> queueHubContext) : IQueueService
{
    public async Task<Queue?> StartQueue(Guid establishmentId, Guid userId, int slots)
    {
        var establishment = await establishmentRepository.GetEstablishmentById(establishmentId);
        
        if(establishment is null)
            throw new NotFoundEstablishmentException("Não foi encontrado estabelecimento para o id informado");
        
        if(establishment.UserId != userId)
            throw new UnauthorizedEstablishmentException("Usuário não autorizado a iniciar fila neste estabelecimento");
        
        var queue = await queueRepository.StartQueue(establishmentId, userId, slots);

        return queue;
    }

    public async Task StopQueue(Guid establishmentId, Guid userId)
    {
        await queueRepository.StopQueue(establishmentId, userId);
    }

    public async Task<QueueUser> EnterQueue(Guid queueId, Guid userId)
    {
        var queue = await queueRepository.GetQueueById(queueId);
        
        if(queue is null)
            throw new NotFoundQueueException("Fila não encontrada para o id informado");
        
        var countQueueUsers = await queueRepository.CountQueueUsers(queueId);
        
        if(countQueueUsers > queue.Slots)
            throw new FullQueueException("O slots para esta fila já estão preenchidos");
        
        var queueUser = await queueRepository.EnterQueue(queueId, userId);
        
        return queueUser;
    }

    public async Task<QueueUser?> GetQueueUser(Guid userId, Guid establishmentId)
    {
        var queueUser = await queueRepository.GetQueueUser(userId, establishmentId);
        
        if (queueUser is null)
            throw new NotFoundQueueUserException("Usuário não encontrado na fila deste estabelecimento");

        return queueUser;
    }

    private async void UpdateAverageTimeEstablishment(Guid establishmentId)
    {
        var queueUsers = await queueRepository.GetFinishedQueueUsersByEstablishmentId(establishmentId);

        var totalMinutes = queueUsers.Sum(q => (q.EndDate.Value - q.StartDate.Value).TotalMinutes);
        
        double averageMinutes = totalMinutes / queueUsers.Count;

        await establishmentRepository.UpdateAverageTimeEstablishment(establishmentId, (int)averageMinutes);
    }

    public async Task<QueueUser?> StartNextQueueUser(Guid establishmentId)
    {
        var establishment = await establishmentRepository.GetEstablishmentById(establishmentId);
         
        if (establishment is null)
            throw new NotFoundEstablishmentException("Não foi encontrado estabelecimento para o id informado");
         
        if(establishment.QueueId is null)
            throw new NotFoundQueueException("O estabelecimento não possui uma fila ativa");
         
        var queue = await queueRepository.GetQueueById(establishment.QueueId.Value);
        
        var queueUser = await queueRepository.GetNextQueueUser(queue.Id);

        queueUser.StartDate = DateTime.UtcNow;
        
        var updatedQueueUser = await queueRepository.UpdateQueueUser(queueUser);

        UpdateAverageTimeEstablishment(establishmentId);
        
        return updatedQueueUser;
    }

    public async Task<int> CountInQueueUsers(Guid queueId)
    {
        return await queueRepository.CountUsersInQueue(queueId);
    }

    public async Task<int> CountServicesToday(Guid establishmentId)
    {
        return await queueRepository.CountServicesToday(establishmentId);
    }

    public async Task<QueueUser?> GetQueueUserById(Guid userId, Guid queueId)
    {
        return await queueRepository.GetQueueUserById(userId, queueId);
    }

    public async Task<QueueUser?> LeaveQueue(Guid userId, Guid queueId)
    {
        var queueUser = await queueRepository.LeaveQueue(userId, queueId);

        await UpdateQueuePositions(queueId);
        
        await queueHubContext.Clients.Group(queueId.ToString()).SendAsync("UpdateQueuePositions", queueId);
        
        return queueUser;
    }

    public async Task UpdateQueuePositions(Guid queueId)
    {
        var queueUsers = await queueRepository.GetQueueUsers(queueId);
    
        for (int i = 0; i < queueUsers.Count; i++)
        {
            int newPosition = i + 1;
            if (queueUsers[i].Position != newPosition)
            {
                queueUsers[i].Position = newPosition;
                await queueRepository.UpdateQueueUser(queueUsers[i]);
            }
        }
    }
}