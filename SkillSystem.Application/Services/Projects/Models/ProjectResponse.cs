namespace SkillSystem.Application.Services.Projects.Models;

public record ProjectResponse : ProjectShortInfo
{
    public ICollection<ProjectRoleResponse> Roles { get; init; }
}