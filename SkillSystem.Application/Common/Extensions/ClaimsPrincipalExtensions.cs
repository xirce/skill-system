using System.Security.Claims;

namespace SkillSystem.Application.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal principal)
    {
        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId is not null ? Guid.Parse(userId) : null;
    }
}
