using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.Application.Services.Departments.Models;

public record GetDepartmentEmployeesResponse(int DepartmentId, IReadOnlyCollection<EmployeeShortInfo> Employees);
