using System.Security.Claims;

namespace SkillSystem.Application.Common.Services;

public interface ICurrentUserProvider
{
    ClaimsPrincipal? User { get; }
}