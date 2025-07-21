using Mapster;
using Microsoft.AspNetCore.Mvc;
using QueueUp.Application.Dtos;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EstablishmentController(IEstablishmentService establishmentService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateEstablishmentDto createEstablishmentDto)
    {
        try
        {
            var parsedEstablishment = createEstablishmentDto.Adapt<Establishment>();
            
            var createdEstablishment = await establishmentService.Create(parsedEstablishment);
            
            return Ok(createdEstablishment);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}