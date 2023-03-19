using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.Application.Services.Employees;

public interface IEmployeesService
{
    Task<EmployeeResponse> GetEmployeeInfoAsync(Guid employeeId);
}
