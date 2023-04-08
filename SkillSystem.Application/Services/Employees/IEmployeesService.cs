using SkillSystem.Application.Common.Models;
using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.Application.Services.Employees;

public interface IEmployeesService
{
    Task<Employee> GetOrCreateEmployeeByUserId(Guid userId);
    Task<PaginatedList<Employee>> SearchEmployees(SearchEmployeesRequest request);
    Task<ICollection<Employee>> GetSubordinates(Guid managerId);
}
