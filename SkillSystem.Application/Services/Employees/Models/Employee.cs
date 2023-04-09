using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Employees.Models;

public class Employee
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public EmployeeType Type { get; set; }
    public Guid? ManagerId { get; set; }
}
