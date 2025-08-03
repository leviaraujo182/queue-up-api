using QueueUp.Application.Exceptions.Establishment;
using QueueUp.Application.Exceptions.Queue;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.Application.Services;

public class QueueService(IQueueRepository queueRepository, IEstablishmentRepository establishmentRepository) : IQueueService
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
}