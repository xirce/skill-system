using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Services.Grades.Models;
using SkillSystem.Application.Services.Roles;
using SkillSystem.Application.Services.Roles.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/roles/{roleId}/grades")]
public class RoleGradesController : BaseController
{
    private readonly IRolesService rolesService;

    public RoleGradesController(IRolesService rolesService)
    {
        this.rolesService = rolesService;
    }

    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<ActionResult<int>> AddGrade(int roleId, AddGradeRequest request)
    {
        var gradeId = await rolesService.AddGradeAsync(roleId, request);
        return Ok(gradeId);
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<GradeShortInfo>>> GetRoleGrades(int roleId)
    {
        var grades = await rolesService.GetRoleGradesAsync(roleId);
        return Ok(grades);
    }

    [HttpGet]
    [Route("~/api/roles/{roleId}/grades-with-skills")]
    public async Task<ActionResult<ICollection<GradeWithSkills>>> GetRoleSkills(int roleId)
    {
        var gradesWithSkills = await rolesService.GetRoleGradesWithSkillsAsync(roleId);
        return Ok(gradesWithSkills);
    }

    [HttpPut("{gradeId}/insert-after")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> InsertGradeAfter(int roleId, int gradeId, [FromQuery] int? prevGradeId = null)
    {
        await rolesService.InsertGradeAfterAsync(roleId, gradeId, prevGradeId);
        return NoContent();
    }

    [HttpDelete("{gradeId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteGrade(int roleId, int gradeId)
    {
        await rolesService.DeleteGradeAsync(roleId, gradeId);
        return NoContent();
    }
}