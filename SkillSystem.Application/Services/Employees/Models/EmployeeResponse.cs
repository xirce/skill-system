namespace SkillSystem.Application.Services.Employees.Models;

public record EmployeeResponse
{
    public Guid Id { get; init; }
    public ManagerInfo? Manager { get; init; }
}
