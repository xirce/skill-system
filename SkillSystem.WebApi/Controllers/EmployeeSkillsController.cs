using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Grading;
using SkillSystem.Application.Services.Grading.Models;
using SkillSystem.Application.Services.Grading.Skills;
using SkillSystem.Application.Services.Grading.Skills.Models;

namespace SkillSystem.WebApi.Controllers;

[Authorize]
[Route("api/employees/{employeeId}/skills")]
public class EmployeeSkillsController : BaseController
{
    private readonly IEmployeeGradingManager employeeGradingManager;
    private readonly IEmployeeSkillsProvider employeeSkillsProvider;

    public EmployeeSkillsController(
        IEmployeeGradingManager employeeGradingManager,
        IEmployeeSkillsProvider employeeSkillsProvider)
    {
        this.employeeGradingManager = employeeGradingManager;
        this.employeeSkillsProvider = employeeSkillsProvider;
    }

    /// <summary>
    /// Отметить скилл изученным сотрудником.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    [HttpPost]
    public async Task AddEmployeeSkill(Guid employeeId, AddEmployeeSkillRequest request)
    {
        request = request with { EmployeeId = employeeId };
        await employeeGradingManager.AddSkillToEmployee(request);
    }

    /// <summary>
    /// Получить детальную информацию о скилле сотрудника.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="skillId">Идентификатор скилла</param>
    /// <returns>Скилл сотрудника вместе с его подскиллами</returns>
    [HttpGet("{skillId}")]
    public async Task<EmployeeSkillResponse> GetEmployeeSkill(Guid employeeId, int skillId)
    {
        return await employeeSkillsProvider.GetEmployeeSkillAsync(employeeId, skillId);
    }

    /// <summary>
    /// Получить скиллы сотрудника.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="roleId">Идентификатор роли</param>
    /// <returns>Скиллы, которые есть у сотрудника</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeSkillShortInfo>>> FindEmployeeSkills(
        Guid employeeId,
        [FromQuery] int? roleId = null)
    {
        var employeeSkills = await employeeSkillsProvider.FindEmployeeSkillsAsync(employeeId, roleId);
        return Ok(employeeSkills);
    }

    /// <summary>
    /// Подтвердить скилл сотрудника.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="skillId">Идентификатор скилла</param>
    /// <returns></returns>
    [HttpPost("{skillId}/approve")]
    public async Task ApproveEmployeeSkill(Guid employeeId, int skillId)
    {
        var request = new ApproveEmployeeSkillRequest(employeeId, skillId);
        await employeeGradingManager.ApproveEmployeeSkill(request);
    }

    /// <summary>
    /// Отметить скилл неизученным сотрудником.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="skillId">Идентификатор скилла</param>
    [HttpDelete("{skillId}")]
    public async Task DeleteEmployeeSkill(Guid employeeId, int skillId)
    {
        var request = new DeleteEmployeeSkillRequest(employeeId, skillId);
        await employeeGradingManager.DeleteEmployeeSkill(request);
    }
}
