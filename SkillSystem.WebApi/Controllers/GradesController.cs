using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Grades;
using SkillSystem.Application.Services.Grades.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/grades")]
public class GradesController : BaseController
{
    private readonly IGradesService gradesService;

    public GradesController(IGradesService gradesService)
    {
        this.gradesService = gradesService;
    }

    /// <summary>
    /// Получить информацию о грейде.
    /// </summary>
    /// <param name="gradeId"></param>
    /// <returns></returns>
    [HttpGet("{gradeId}")]
    public async Task<ActionResult<GradeResponse>> GetGradeById(int gradeId)
    {
        var grade = await gradesService.GetGradeByIdAsync(gradeId);
        return Ok(grade);
    }

    /// <summary>
    /// Поиск грейдов по названию.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<GradeShortInfo>>> FindGrades(
        [FromQuery] SearchGradesRequest request)
    {
        var grades = await gradesService.FindGradesAsync(request);
        return Ok(grades);
    }

    /// <summary>
    /// Изменить информацию о грейде.
    /// </summary>
    /// <param name="gradeId">Идентификатор грейда</param>
    [HttpPut("{gradeId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> UpdateGrade(int gradeId, GradeRequest request)
    {
        await gradesService.UpdateGradeAsync(gradeId, request);
        return NoContent();
    }
}
