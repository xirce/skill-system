using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Grades;
using SkillSystem.Application.Services.Positions.Models;

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

    [HttpPut("add/{positionId}")]
    public async Task<IActionResult> AddGradePosition(int gradeId, int positionId)
    {
        await gradesService.AddGradePositionAsync(gradeId, positionId);
        return NoContent();
    }

    [HttpDelete("{positionId}")]
    public async Task<IActionResult> DeleteGradePosition(int gradeId, int positionId)
    {
        await gradesService.DeleteGradePositionAsync(gradeId, positionId);
        return NoContent();
    }
}