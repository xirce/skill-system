using SkillSystem.Application.Common.Models.Requests;

namespace SkillSystem.Application.Services.Roles.Models;

public record SearchRolesRequest : PaginationQuery
{
    public string? Title { get; init; }
}