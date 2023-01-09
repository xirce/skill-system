namespace SkillSystem.Application.Services.Projects.Models;

public record ProjectRoleRequest
{
    public int ProjectId { get; init; }
    public int RoleId { get; init; }
    public string? EmployeeId { get; init; }
}