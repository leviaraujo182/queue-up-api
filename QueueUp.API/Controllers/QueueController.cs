using Mapster;
using Microsoft.AspNetCore.Mvc;
using QueueUp.Application.Dtos;
using QueueUp.Application.Extensions;
using QueueUp.Application.Services;
using QueueUp.Domain.Interfaces;

namespace QueueUp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class QueueController(IQueueService queueService) : ControllerBase
{
    [HttpPost("{id}/StartQueue")]
    public async Task<IActionResult> StartQueue(Guid id, StartQueueDto startQueueDto)
    {
        try
        {
            var userId = User.GetUserId();
            
            if(userId is null)
                return Unauthorized(new { Message = "Usuário não encontrado" });
            
            var parsedUserId = Guid.Parse(userId);
            
            var queue = await queueService.StartQueue(id, parsedUserId, startQueueDto.Slots);
            
            if (queue == null)
                return NotFound(new { Message = "Fila não encontrada ou estabelecimento inválido" });
            
            return Ok(queue);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    [HttpPost("{id}/StopQueue")]
    public async Task<IActionResult> StopQueue(Guid id)
    {
        try
        {
            var userId = User.GetUserId();
            
            if(userId is null)
                return Unauthorized(new { Message = "Usuário não encontrado" });
            
            var parsedUserId = Guid.Parse(userId);
            
            await queueService.StopQueue(id, parsedUserId);
            
            return Ok(new { Message = "Fila parada com sucesso" });
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    [HttpPost("{id}/EnterQueue")]
    public async Task<IActionResult> EnterQueue(Guid id)
    {
        try
        {
            var userId = User.GetUserId();
            
            if(userId is null)
                return Unauthorized(new { Message = "Usuário não encontrado" });
            
            var parsedUserId = Guid.Parse(userId);
            
            var queueUser = await queueService.EnterQueue(id, parsedUserId);
            
            var parsedQueueUser = queueUser.Adapt<QueueUserDto>();
            
            return Ok(parsedQueueUser);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
}