using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Services.Grades;
using SkillSystem.Application.Services.Positions.Models;
using SkillSystem.WebApi.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/grades/{gradeId}/positions")]
public class GradePositionsController : BaseController
{
    private readonly IGradesService gradesService;

    public GradePositionsController(IGradesService gradesService)
    {
        this.gradesService = gradesService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PositionResponse>>> GetGradePositions(int gradeId)
    {
        var positions = await gradesService.GetGradePositionsAsync(gradeId);
        return Ok(positions);
    }

    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> AddGradePosition(int gradeId, [FromBody] GradePositionRequest positionRequest)
    {
        await gradesService.AddGradePositionAsync(gradeId, positionRequest.PositionId);
        return Ok(positionRequest.PositionId);
    }

    [HttpDelete("{positionId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteGradePosition(int gradeId, int positionId)
    {
        await gradesService.DeleteGradePositionAsync(gradeId, positionId);
        return NoContent();
    }
}
