using SkillSystem.Application.Common.Models.Requests;

namespace SkillSystem.Application.Repositories.Projects;

public record ProjectRoleFilter : PaginationQuery
{
    public int[]? ProjectIds { get; init; }
    public int[]? RoleIds { get; init; }
    public bool? IsFree { get; init; }
}
