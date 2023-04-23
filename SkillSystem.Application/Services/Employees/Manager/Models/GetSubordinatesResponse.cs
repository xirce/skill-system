using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.Application.Services.Employees.Manager.Models;

public record GetSubordinatesResponse(Employee Manager, ICollection<Employee> Subordinates);
