using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Departments;
using SkillSystem.Application.Services.Departments.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/departments/{departmentId}/employees")]
public class DepartmentEmployeesController : BaseController
{
    private readonly IDepartmentsService departmentsService;

    public DepartmentEmployeesController(IDepartmentsService departmentsService)
    {
        this.departmentsService = departmentsService;
    }

    /// <summary>
    /// Получить всех сотрудников какого-либо отдела.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела</param>
    [HttpGet]
    public async Task<GetDepartmentEmployeesResponse> GetDepartmentEmployees(int departmentId)
    {
        return await departmentsService.GetDepartmentEmployees(departmentId);
    }

    /// <summary>
    /// Добавить сотрудника в отдел.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела</param>
    [HttpPost]
    public async Task AddEmployeeToDepartment(int departmentId, AddEmployeeToDepartmentRequest request)
    {
        request = request with { DepartmentId = departmentId };
        await departmentsService.AddEmployeeToDepartment(request);
    }

    /// <summary>
    /// Удалить сотрудника из отдела.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела</param>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    [HttpDelete("{employeeId}")]
    public async Task RemoveEmployeeFromDepartment(int departmentId, Guid employeeId)
    {
        var request = new RemoveEmployeeFromDepartmentRequest(employeeId, departmentId);
        await departmentsService.RemoveEmployeeFromDepartment(request);
    }
}
