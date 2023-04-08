using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.Application.Services.Employees.Manager.Models;

public record GetSubordinatesResponse
{
    public Employee Manager { get; init; }
    public IReadOnlyCollection<Employee> Subordinates { get; init; }
}
