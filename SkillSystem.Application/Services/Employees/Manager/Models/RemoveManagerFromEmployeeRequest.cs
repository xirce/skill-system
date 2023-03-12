namespace SkillSystem.Application.Services.Employees.Manager.Models;

public record RemoveManagerFromEmployeeRequest
{
    public string EmployeeId { get; init; }
}
