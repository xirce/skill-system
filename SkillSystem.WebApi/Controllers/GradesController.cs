using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("{gradeId}")]
    public async Task<ActionResult<GradeResponse>> GetGradeById(int gradeId)
    {
        var grade = await gradesService.GetGradeByIdAsync(gradeId);
        return Ok(grade);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<GradeShortInfo>>> FindGrades(
        [FromQuery] SearchGradesRequest request
    )
    {
        var grades = await gradesService.FindGradesAsync(request);
        return Ok(grades);
    }

    [HttpPut("{gradeId}")]
    public async Task<IActionResult> UpdateGrade(int gradeId, GradeRequest request)
    {
        await gradesService.UpdateGradeAsync(gradeId, request);
        return NoContent();
    }
}