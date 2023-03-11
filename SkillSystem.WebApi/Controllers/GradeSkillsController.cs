using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Services.Grades;
using SkillSystem.Application.Services.Skills.Models;
using SkillSystem.WebApi.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/grades/{gradeId}/skills")]
public class GradeSkillsController : BaseController
{
    private readonly IGradesService gradesService;

    public GradeSkillsController(IGradesService gradesService)
    {
        this.gradesService = gradesService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SkillResponse>>> GetGradeSkills(int gradeId)
    {
        var skills = await gradesService.GetGradeSkillsAsync(gradeId);
        return Ok(skills);
    }

    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> AddGradeSkill(int gradeId, GradeSkillRequest skillRequest)
    {
        await gradesService.AddGradeSkillAsync(gradeId, skillRequest.SkillId);
        return Ok(skillRequest.SkillId);
    }

    [HttpDelete("{skillId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteGradeSkill(int gradeId, int skillId)
    {
        await gradesService.DeleteGradeSkillAsync(gradeId, skillId);
        return NoContent();
    }
}
