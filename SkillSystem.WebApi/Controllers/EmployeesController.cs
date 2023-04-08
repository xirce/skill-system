using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Employees;
using SkillSystem.Application.Services.Employees.Manager.Models;
using SkillSystem.Application.Services.Employees.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/employees")]
public class EmployeesController : BaseController
{
    private readonly IEmployeesManager employeesManager;

    public EmployeesController(IEmployeesManager employeesManager)
    {
        this.employeesManager = employeesManager;
    }

    /// <summary>
    /// Поиск по сотрудникам.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Список найденных сотрудников</returns>
    [HttpGet("search")]
    public async Task<PaginatedResponse<Employee>> SearchEmployees([FromQuery] SearchEmployeesRequest request)
    {
        return await employeesManager.SearchEmployees(request);
    }

    /// <summary>
    /// Получить информацию о сотруднике.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <returns>Информация о сотруднике</returns>
    [HttpGet("{employeeId}")]
    public async Task<GetEmployeeInfoResponse> GetEmployeeInfo(Guid employeeId)
    {
        return await employeesManager.GetEmployeeInfo(employeeId);
    }

    /// <summary>
    /// Получить подчинённых руководителя.
    /// </summary>
    /// <param name="employeeId">Идентификатор руководителя</param>
    /// <returns>Подчинённые руководителя</returns>
    [HttpGet("{employeeId}/subordinates")]
    public async Task<GetSubordinatesResponse> GetSubordinates(Guid employeeId)
    {
        return await employeesManager.GetSubordinates(employeeId);
    }
}
