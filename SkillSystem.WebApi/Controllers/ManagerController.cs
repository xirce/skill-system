using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Employees.Manager;
using SkillSystem.Application.Services.Employees.Manager.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/employees/{employeeId}/manager")]
public class ManagerController : BaseController
{
    private readonly IManagerService managerService;

    public ManagerController(IManagerService managerService)
    {
        this.managerService = managerService;
    }

    /// <summary>
    /// Назначить сотруднику руководителя.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника, которому назначится руководитель</param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task SetManagerForEmployee(Guid employeeId, SetManagerRequest request)
    {
        request = request with { EmployeeId = employeeId };
        await managerService.SetManagerAsync(request);
    }

    /// <summary>
    /// Отвязать от сотрудника руководителя.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <returns></returns>
    [HttpDelete]
    public async Task RemoveManagerFromEmployee(Guid employeeId)
    {
        await managerService.RemoveManagerFromEmployeeAsync(employeeId);
    }
}
