using System.Security.Claims;
using SkillSystem.Application.Common.Services;

namespace SkillSystem.WebApi.Services;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
}