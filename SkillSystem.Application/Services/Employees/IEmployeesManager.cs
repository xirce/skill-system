using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Employees.Manager.Models;
using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.Application.Services.Employees;

public interface IEmployeesManager
{
    Task<GetEmployeeInfoResponse> GetEmployeeInfo(Guid employeeId);
    Task<PaginatedResponse<Employee>> SearchEmployees(SearchEmployeesRequest request);
    Task<GetSubordinatesResponse> GetSubordinates(Guid managerId);
}
