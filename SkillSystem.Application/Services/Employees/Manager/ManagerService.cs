using SkillSystem.Application.Repositories.Employees;
using SkillSystem.Application.Services.Employees.Manager.Models;

namespace SkillSystem.Application.Services.Employees.Manager;

public class ManagerService : IManagerService
{
    private readonly IManagerRepository managerRepository;

    public ManagerService(IManagerRepository managerRepository)
    {
        this.managerRepository = managerRepository;
    }

    // TODO: Добавить проверку на отсутствие цикличной зависимости
    public async Task SetManagerForEmployeeAsync(SetManagerForEmployeeRequest request)
    {
        await managerRepository.SetManagerForEmployeeAsync(request.EmployeeId, request.ManagerId);
    }

    public async Task RemoveManagerFromEmployeeAsync(Guid employeeId)
    {
        await managerRepository.RemoveManagerFromEmployeeAsync(employeeId);
    }
}
