namespace SkillSystem.Application.Services.Employees.Manager.Models;

public record SetManagerForEmployeeRequest
{
    public Guid EmployeeId { get; init; }
    public Guid ManagerId { get; init; }
}
