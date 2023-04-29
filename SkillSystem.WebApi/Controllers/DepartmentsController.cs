using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Departments;
using SkillSystem.Application.Services.Departments.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/departments")]
public class DepartmentsController : BaseController
{
    private readonly IDepartmentsService departmentsService;

    public DepartmentsController(IDepartmentsService departmentsService)
    {
        this.departmentsService = departmentsService;
    }

    /// <summary>
    /// Создать отдел.
    /// </summary>
    [HttpPost]
    public async Task<int> CreateDepartment(CreateDepartmentRequest request)
    {
        return await departmentsService.CreateDepartment(request);
    }

    /// <summary>
    /// Получить информацию об отделе.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела</param>
    /// <returns></returns>
    [HttpGet("{departmentId}")]
    public async Task<DepartmentResponse> GetDepartment(int departmentId)
    {
        return await departmentsService.GetDepartmentById(departmentId);
    }

    /// <summary>
    /// Получить все отделы.
    /// </summary>
    [HttpGet]
    public async Task<IReadOnlyCollection<DepartmentResponse>> GetAllDepartments()
    {
        return await departmentsService.GetAllDepartments();
    }

    /// <summary>
    /// Установить руководителя отдела.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела</param>
    [HttpPost("{departmentId}/head")]
    public async Task SetHeadEmployee(int departmentId, SetHeadEmployeeRequest request)
    {
        request = request with { DepartmentId = departmentId };
        await departmentsService.SetHeadEmployee(request);
    }

    /// <summary>
    /// Изменить информацию об отделе.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела</param>
    [HttpPut("{departmentId}")]
    public async Task UpdateDepartment(int departmentId, UpdateDepartmentRequest request)
    {
        request = request with { DepartmentId = departmentId };
        await departmentsService.UpdateDepartment(request);
    }


    /// <summary>
    /// Удалить отдел.
    /// </summary>
    /// <param name="departmentId">Идентификатор отдела</param>
    [HttpDelete("{departmentId}")]
    public async Task DeleteDepartment(int departmentId)
    {
        await departmentsService.DeleteDepartment(departmentId);
    }
}
