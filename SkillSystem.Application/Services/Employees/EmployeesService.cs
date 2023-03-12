using SkillSystem.Application.Repositories.Employees;
using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.Application.Services.Employees;

public class EmployeesService : IEmployeesService
{
    private readonly IManagerRepository managerRepository;

    public EmployeesService(IManagerRepository managerRepository)
    {
        this.managerRepository = managerRepository;
    }

    public async Task<EmployeeResponse> GetEmployeeInfoAsync(Guid employeeId)
    {
        var employeeManager = await managerRepository.FindEmployeeManagerAsync(employeeId);
        var managerId = employeeManager?.ManagerId;
        return new EmployeeResponse
        {
            Id = employeeId,
            Manager = managerId.HasValue ? new ManagerInfo { Id = managerId.Value } : null
        };
    }
}
