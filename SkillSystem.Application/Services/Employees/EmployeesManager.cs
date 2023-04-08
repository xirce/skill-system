using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Employees.Manager.Models;
using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.Application.Services.Employees;

public class EmployeesManager : IEmployeesManager
{
    private readonly IEmployeesService employeesService;

    public EmployeesManager(IEmployeesService employeesService)
    {
        this.employeesService = employeesService;
    }

    public async Task<GetEmployeeInfoResponse> GetEmployeeInfo(Guid employeeId)
    {
        var employee = await employeesService.GetOrCreateEmployeeByUserId(employeeId);
        return employee.Adapt<GetEmployeeInfoResponse>();
    }

    public async Task<PaginatedResponse<Employee>> SearchEmployees(SearchEmployeesRequest request)
    {
        var employees = await employeesService.SearchEmployees(request);
        return employees.ToResponse();
    }

    public async Task<GetSubordinatesResponse> GetSubordinates(Guid managerId)
    {
        var manager = await employeesService.GetOrCreateEmployeeByUserId(managerId);
        var subordinates = await employeesService.GetSubordinates(manager.Id);
        return new GetSubordinatesResponse
        {
            ManagerId = manager.Id,
            Subordinates = subordinates.Adapt<IReadOnlyCollection<Employee>>()
        };
    }
}
