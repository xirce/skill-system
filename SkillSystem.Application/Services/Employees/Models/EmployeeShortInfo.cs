using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Employees.Models;

public class EmployeeShortInfo
{
    public Guid Id { get; set; }
    public EmployeeType Type { get; set; }
    public Guid? ManagerId { get; set; }
}
