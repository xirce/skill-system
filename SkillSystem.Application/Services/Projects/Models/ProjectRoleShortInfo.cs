using SkillSystem.Application.Services.Employees.Models;
using SkillSystem.Application.Services.Roles.Models;

namespace SkillSystem.Application.Services.Projects.Models;

public record ProjectRoleShortInfo
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public RoleShortInfo Role { get; init; }
    public EmployeeShortInfo? Employee { get; init; }
}
