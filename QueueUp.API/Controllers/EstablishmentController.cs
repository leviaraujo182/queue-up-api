using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueueUp.Application.Dtos;
using QueueUp.Application.Extensions;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EstablishmentController(IEstablishmentService establishmentService, IQueueService queueService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateEstablishmentDto createEstablishmentDto)
    {
        try
        {
            var userId = User.GetUserId();
            
            if(userId is null)
                return Unauthorized(new { Message = "Usuário não encontrado" });
            
            var userIdGuid = Guid.Parse(userId);
            
            var parsedEstablishment = createEstablishmentDto.Adapt<Establishment>();
            
            parsedEstablishment.UserId = userIdGuid;
            
            var createdEstablishment = await establishmentService.Create(parsedEstablishment);
            
            var establishmentDto = createdEstablishment.Adapt<EstablishmentDto>();
            
            return Ok(establishmentDto);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("GetEstablishmentsMetrics")]
    public async Task<IActionResult> GetEstablishmentMetrics()
    {
        try
        {
            var userId = User.GetUserId();
            
            if(userId is null)
                return Unauthorized(new { Message = "Usuário não encontrado" });
            
            var parsedUserId = Guid.Parse(userId);
            
            var metrics = await establishmentService.GetEstablishmentsMetrics(parsedUserId);
            
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("GetEstablishmentsByOwner")]
    public async Task<IActionResult> GetEstablishmentsByOwner([FromQuery] EstablishmentFiltersDto establishmentFiltersDto)
    {
        try
        {
            var userId = User.GetUserId();
            
            if(userId is null)
                return Unauthorized(new { Message = "Usuário não encontrado" });
            
            var parsedUserId = Guid.Parse(userId);
            
            var establishmentFilters = establishmentFiltersDto.Adapt<EstablishmentFilters>();
            
            var establishments = await establishmentService.GetEstablishmentsByOwnerId(parsedUserId, establishmentFilters);
            
            return Ok(establishments);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEstablishmentById(Guid id)
    {
        try
        {
            var establishment = await establishmentService.GetEstablishmentById(id);
            
            if (establishment == null)
                return NotFound(new { Message = "Estabelecimento não encontrado" });
            
            var establishmentDto = establishment.Adapt<EstablishmentDto>();

            var inQueueUser = await queueService.CountInQueueUsers(establishment.QueueId.Value, id);
            
            establishmentDto.InQueueUsers = inQueueUser;
            
            return Ok(establishmentDto);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    [HttpPost("{id}/Rating")]
    public async Task<IActionResult> RateEstablishment(Guid id, [FromBody] CreateEstablishmentRatingDto createEstablishmentRatingDto)
    {
        try
        {
            var userId = User.GetUserId();
            
            if(userId is null)
                return Unauthorized(new { Message = "Usuário não encontrado" });
            
            var parsedUserId = Guid.Parse(userId);
            
            var rating = await establishmentService.CreateRating(parsedUserId, id, createEstablishmentRatingDto.Rating);
            
            var parsedRating = rating.Adapt<EstablishmentRatingDto>();
            
            return Ok(parsedRating);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
}