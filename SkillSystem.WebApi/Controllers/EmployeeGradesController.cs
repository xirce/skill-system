using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SkillSystem.Application.Services.Grading;
using SkillSystem.Application.Services.Grading.Grades;
using SkillSystem.Application.Services.Grading.Grades.Models;
using SkillSystem.Application.Services.Grading.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/employees/{employeeId}/grades")]
public class EmployeeGradesController : BaseController
{
    private readonly IEmployeeGradingManager employeeGradingManager;
    private readonly IEmployeeGradesProvider employeeGradesProvider;

    public EmployeeGradesController(
        IEmployeeGradingManager employeeGradingManager,
        IEmployeeGradesProvider employeeGradesProvider)
    {
        this.employeeGradingManager = employeeGradingManager;
        this.employeeGradesProvider = employeeGradesProvider;
    }

    /// <summary>
    /// Повысить грейд сотрудника.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="request">Идентификатор грейда</param>
    [HttpPost]
    public async Task GradeEmployee(Guid employeeId, GradeEmployeeRequest request)
    {
        request = request with { EmployeeId = employeeId };
        await employeeGradingManager.GradeEmployee(request);
    }

    /// <summary>
    /// Получить грейды сотрудника.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="roleId">Идентификатор роли</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IReadOnlyCollection<EmployeeGradeResponse>> GetEmployeeGrades(
        Guid employeeId,
        [FromQuery] int? roleId = null)
    {
        return await employeeGradesProvider.FindEmployeeGradesAsync(employeeId, roleId);
    }

    /// <summary>
    /// Получить текущий грейд сотрудника по роли.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="roleId">Идентификатор роли</param>
    /// <returns>Наивысший грейд сотрудника в определенной роли</returns>
    [HttpGet("actual")]
    public async Task<EmployeeGradeResponse> GetEmployeeActualGrade(
        Guid employeeId,
        [FromQuery] [BindRequired] int roleId)
    {
        return await employeeGradesProvider.GetRoleActualGradeAsync(employeeId, roleId);
    }

    /// <summary>
    /// Подтвердить грейд сотрудника.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="gradeId">Идентификатор грейда</param>
    [HttpPost("{gradeId}/approve")]
    public async Task ApproveEmployeeGrade(Guid employeeId, int gradeId)
    {
        var request = new ApproveGradeRequest(employeeId, gradeId);
        await employeeGradingManager.ApproveEmployeeGrade(request);
    }
}
