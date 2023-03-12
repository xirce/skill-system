using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Employees;
using SkillSystem.Application.Services.Employees.Manager;
using SkillSystem.Application.Services.Employees.Manager.Models;
using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/employees/{employeeId}")]
public class EmployeesController : BaseController
{
    private readonly IManagerService managerService;
    private readonly IEmployeesService employeesService;

    public EmployeesController(IManagerService managerService, IEmployeesService employeesService)
    {
        this.managerService = managerService;
        this.employeesService = employeesService;
    }

    /// <summary>
    /// Назначить сотруднику руководителя.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника, которому назначится руководитель</param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("manager")]
    public async Task<IActionResult> SetManagerForEmployee(Guid employeeId, SetManagerForEmployeeRequest request)
    {
        request = request with { EmployeeId = employeeId };
        await managerService.SetManagerForEmployeeAsync(request);
        return NoContent();
    }

    /// <summary>
    /// Отвязать от сотрудника руководителя.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <returns></returns>
    [HttpDelete("manager")]
    public async Task<IActionResult> RemoveManagerFromEmployee(Guid employeeId)
    {
        await managerService.RemoveManagerFromEmployeeAsync(employeeId);
        return NoContent();
    }

    /// <summary>
    /// Получить информацию о сотруднике.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <returns>Информация о сотруднике</returns>
    [HttpGet]
    public async Task<ActionResult<EmployeeResponse>> GetEmployeeInfo(Guid employeeId)
    {
        var employee = await employeesService.GetEmployeeInfoAsync(employeeId);
        return Ok(employee);
    }
}
