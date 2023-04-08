using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Employees;
using SkillSystem.Application.Services.Employees.Manager.Models;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Employees.Manager;

public class ManagerService : IManagerService
{
    private readonly IEmployeesRepository employeesRepository;
    private readonly IUnitOfWork unitOfWork;

    public ManagerService(IEmployeesRepository employeesRepository, IUnitOfWork unitOfWork)
    {
        this.employeesRepository = employeesRepository;
        this.unitOfWork = unitOfWork;
    }

    // TODO: Добавить проверку на отсутствие цикличной зависимости
    public async Task SetManagerAsync(SetManagerRequest request)
    {
        if (request.EmployeeId == request.ManagerId)
            throw new InvalidOperationException("Employee can not be manager for himself");

        var manager = await employeesRepository.GetEmployeeById(request.ManagerId);
        manager.Type = EmployeeType.Manager;
        employeesRepository.UpdateEmployee(manager);

        var employee = await employeesRepository.GetEmployeeById(request.EmployeeId);
        employee.ManagerId = manager.Id;
        employeesRepository.UpdateEmployee(employee);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveManagerFromEmployeeAsync(Guid employeeId)
    {
        var employee = await employeesRepository.GetEmployeeById(employeeId);

        if (employee.ManagerId.HasValue)
            await ChangeEmployeeTypeIfNeededAsync(employee.ManagerId.Value);

        employee.ManagerId = null;
        employeesRepository.UpdateEmployee(employee);
        await unitOfWork.SaveChangesAsync();
    }

    private async Task ChangeEmployeeTypeIfNeededAsync(Guid managerId)
    {
        var subordinatesCount = (await employeesRepository.GetSubordinates(managerId)).Count;
        if (subordinatesCount > 1)
            return;

        var manager = await employeesRepository.GetEmployeeById(managerId);
        manager.Type = EmployeeType.Default;
        employeesRepository.UpdateEmployee(manager);
    }
}
