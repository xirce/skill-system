using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Grading.AddSkillToEmployee;
using SkillSystem.Application.Services.EmployeeSkills.Models;
using SkillSystem.WebApi.Models;

namespace SkillSystem.WebApi.Controllers;

[Authorize]
[Route("api/employees/{employeeId}/skills")]
public class EmployeeSkillsController : BaseController
{
    private readonly ISender sender;

    public EmployeeSkillsController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpPost]
    public async Task AddEmployeeSkill(Guid employeeId, [FromBody] AddSkillsToEmployeeRequest request)
    {
        var command = request.Adapt<AddSkillsToEmployeeCommand>() with { EmployeeId = employeeId };
        await sender.Send(command);
    }

    [HttpGet("{skillId}")]
    public async Task<EmployeeSkillResponse> GetEmployeeSkill(Guid employeeId, int skillId)
    {
        var employeeSkill = await sender.Get(employeeId, skillId);
    }
    //
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<EmployeeSkillShortInfo>>> FindEmployeeSkills(
    //     Guid employeeId,
    //     [FromQuery] int? roleId = null)
    // {
    //     var employeeSkills = await sender.FindEmployeeSkillsAsync(employeeId, roleId);
    //     return Ok(employeeSkills);
    // }
    //
    // [HttpPost("{skillId}/approve")]
    // [Authorize(Roles = AuthRoleNames.Admin)]
    // public async Task<IActionResult> ApproveEmployeeSkill(Guid employeeId, int skillId)
    // {
    //     await sender.ApproveSkillsAsync(employeeId, new[] { skillId });
    //     return NoContent();
    // }
    //
    // [HttpDelete("{skillId}")]
    // public async Task<IActionResult> DeleteEmployeeSkill(Guid employeeId, int skillId)
    // {
    //     await sender.DeleteEmployeeSkillsAsync(employeeId, new[] { skillId });
    //     return NoContent();
    // }
}
