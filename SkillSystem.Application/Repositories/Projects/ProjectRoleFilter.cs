namespace SkillSystem.Application.Repositories.Projects;

public record ProjectRoleFilter
{
    public string? RoleName { get; init; }
    public bool? IsFree { get; init; }
}