using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Grades;
using SkillSystem.Application.Services.Skills.Models;

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
    public async Task<ActionResult<SkillResponse>> GetGradeSkills(int gradeId)
    {
        var skills = await gradesService.GetGradeSkillsAsync(gradeId);
        return Ok(skills);
    }

    [HttpPut("add/{skillId}")]
    public async Task<IActionResult> AddGradeSkill(int gradeId, int skillId)
    {
        await gradesService.AddGradeSkillAsync(gradeId, skillId);
        return NoContent();
    }

    [HttpDelete("{skillId}")]
    public async Task<IActionResult> DeleteGradeSkill(int gradeId, int skillId)
    {
        await gradesService.DeleteGradeSkillAsync(gradeId, skillId);
        return NoContent();
    }
}