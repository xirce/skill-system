using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Employees.Models;

public record GetEmployeeInfoResponse
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Patronymic { get; init; }
    public EmployeeType Type { get; init; }
    public Guid? ManagerId { get; init; }
}
