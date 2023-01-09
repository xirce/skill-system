namespace SkillSystem.Application.Services.Projects.Models;

public record ProjectRoleResponse
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public int RoleId { get; init; }
    public string? EmployeeId { get; init; }
    public bool IsFree { get; init; }
}