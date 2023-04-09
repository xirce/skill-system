namespace SkillSystem.Application.Services.Employees.Manager.Models;

public record SetManagerRequest
{
    public Guid EmployeeId { get; init; }
    public Guid ManagerId { get; init; }
}
