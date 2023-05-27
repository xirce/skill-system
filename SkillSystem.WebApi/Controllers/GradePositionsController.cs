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

    /// <summary>
    /// Получить должности, которые можно занимать на грейде.
    /// </summary>
    /// <param name="gradeId">Идентификатор грейда</param>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PositionResponse>>> GetGradePositions(int gradeId)
    {
        var positions = await gradesService.GetGradePositionsAsync(gradeId);
        return Ok(positions);
    }

    /// <summary>
    /// Закрепить должность на грейде.
    /// </summary>
    /// <param name="gradeId">Идентификатор грейда</param>
    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> AddGradePosition(int gradeId, [FromBody] GradePositionRequest positionRequest)
    {
        await gradesService.AddGradePositionAsync(gradeId, positionRequest.PositionId);
        return Ok(positionRequest.PositionId);
    }

    /// <summary>
    /// Открепить должность от грейда.
    /// </summary>
    /// <param name="gradeId">Идентификатор грейда</param>
    /// <param name="positionId">Идентификатор должности</param>
    [HttpDelete("{positionId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteGradePosition(int gradeId, int positionId)
    {
        await gradesService.DeleteGradePositionAsync(gradeId, positionId);
        return NoContent();
    }
}
