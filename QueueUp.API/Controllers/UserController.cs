using Mapster;
using Microsoft.AspNetCore.Mvc;
using QueueUp.Application.Dtos;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserServices userServices) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        try
        {
            var parsedUser = createUserDto.Adapt<User>();

            var createdUser = await userServices.CreateAsync(parsedUser);

            var createdUserResponse = createdUser.Adapt<UserDto>();
            
            return Ok(createdUserResponse);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var user = await userServices.FindByIdAsync(id);

            if (user == null)
                return NotFound(new { Message = "Usuário não encontrado" });

            var userResponse = user.Adapt<UserDto>();

            return Ok(userResponse);
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}