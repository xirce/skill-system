using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.EmployeeSkills;
using SkillSystem.Application.Services.EmployeeSkills.Models;

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

    [HttpPut("add/{skillId}")]
    public async Task<IActionResult> AddEmployeeSkill(string employeeId, int skillId)
    {
        await employeeSkillsService.AddEmployeeSkillAsync(employeeId, skillId);
        return NoContent();
    }

    [HttpGet("{skillId}")]
    public async Task<ActionResult<EmployeeSkillResponse>> GetEmployeeSkill(string employeeId, int skillId)
    {
        var employeeSkill = await employeeSkillsService.GetEmployeeSkillAsync(employeeId, skillId);
        return Ok(employeeSkill);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeSkillShortInfo>>> FindEmployeeRoleSkills(
        string employeeId,
        [FromQuery] int? roleId = null
    )
    {
        var employeeSkills = await employeeSkillsService.FindEmployeeSkillsAsync(employeeId, roleId);
        return Ok(employeeSkills);
    }

    [HttpGet]
    [Route("~/api/employees/{employeeId}/skills-statuses")]
    public async Task<ActionResult<IEnumerable<EmployeeSkillStatus>>> FindEmployeeRoleSkillsStatuses(
        string employeeId,
        [FromQuery] int roleId
    )
    {
        var employeeSkills = await employeeSkillsService.FindEmployeeSkillsStatusesAsync(employeeId, roleId);
        return Ok(employeeSkills);
    }

    [HttpPut("{skillId}")]
    public async Task<IActionResult> SetSkillApproved(string employeeId, int skillId, bool isApproved)
    {
        await employeeSkillsService.SetSkillApprovedAsync(employeeId, skillId, isApproved);
        return NoContent();
    }

    [HttpDelete("{skillId}")]
    public async Task<IActionResult> DeleteEmployeeSkill(string employeeId, int skillId)
    {
        await employeeSkillsService.DeleteEmployeeSkillAsync(employeeId, skillId);
        return NoContent();
    }
}