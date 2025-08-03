using System.Security.Claims;

namespace QueueUp.Application.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst("userId")?.Value;
    }
}