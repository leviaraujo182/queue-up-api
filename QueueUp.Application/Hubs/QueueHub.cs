using Microsoft.AspNetCore.SignalR;

namespace QueueUp.Application.Hubs;

public class QueueHub : Hub
{
    public async Task JoinQueueGroup(string queueId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, queueId);
    }
}