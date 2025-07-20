using Mapster;
using Microsoft.AspNetCore.Mvc;
using QueueUp.Application.Dtos;
using QueueUp.Domain.Entities;
using QueueUp.Domain.Interfaces;

namespace QueueUp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            var parsedLogin = loginDto.Adapt<Login>();

            var token = await authService.LoginAsync(parsedLogin);
            
            return Ok(new { token = token });
        }
        catch(Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}