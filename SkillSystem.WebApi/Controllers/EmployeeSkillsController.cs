using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Services.EmployeeSkills;
using SkillSystem.Application.Services.EmployeeSkills.Models;
using SkillSystem.WebApi.Models;

namespace SkillSystem.WebApi.Controllers;

[Authorize]
[Route("api/employees/{employeeId}/skills")]
public class EmployeeSkillsController : BaseController
{
    private readonly IEmployeeSkillsService employeeSkillsService;

    public EmployeeSkillsController(IEmployeeSkillsService employeeSkillsService)
    {
        this.employeeSkillsService = employeeSkillsService;
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployeeSkill(Guid employeeId, EmployeeSkillRequest skillRequest)
    {
        await employeeSkillsService.AddEmployeeSkillsAsync(employeeId, new[] { skillRequest.SkillId });
        return Ok(skillRequest.SkillId);
    }

    [HttpGet("{skillId}")]
    public async Task<ActionResult<EmployeeSkillResponse>> GetEmployeeSkill(Guid employeeId, int skillId)
    {
        var employeeSkill = await employeeSkillsService.GetEmployeeSkillAsync(employeeId, skillId);
        return Ok(employeeSkill);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeSkillShortInfo>>> FindEmployeeSkills(
        Guid employeeId,
        [FromQuery] int? roleId = null)
    {
        var employeeSkills = await employeeSkillsService.FindEmployeeSkillsAsync(employeeId, roleId);
        return Ok(employeeSkills);
    }

    [HttpPost("{skillId}/approve")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> ApproveEmployeeSkill(Guid employeeId, int skillId)
    {
        await employeeSkillsService.ApproveSkillsAsync(employeeId, new[] { skillId });
        return NoContent();
    }

    [HttpDelete("{skillId}")]
    public async Task<IActionResult> DeleteEmployeeSkill(Guid employeeId, int skillId)
    {
        await employeeSkillsService.DeleteEmployeeSkillsAsync(employeeId, new[] { skillId });
        return NoContent();
    }
}
