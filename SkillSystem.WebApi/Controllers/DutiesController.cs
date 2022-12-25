using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Duties.Filters;
using SkillSystem.Application.Services.Duties;
using SkillSystem.Application.Services.Duties.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/duties")]
public class DutiesController : BaseController
{
    private readonly IDutiesService dutiesService;

    public DutiesController(IDutiesService dutiesService)
    {
        this.dutiesService = dutiesService;
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateDuty(DutyRequest request)
    {
        var dutyId = await dutiesService.CreateDutyAsync(request);
        return Ok(dutyId);
    }

    [HttpGet("{dutyId}")]
    public async Task<ActionResult<DutyResponse>> GetDutyById(int dutyId)
    {
        var duty = await dutiesService.GetDutyByIdAsync(dutyId);
        return Ok(duty);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<DutyShortInfo>>> FindDuties(
        [FromQuery] PaginationQuery<DutyFilter> query
    )
    {
        var duties = await dutiesService.FindDutiesAsync(query);
        return Ok(duties);
    }

    [HttpPut("{dutyId}")]
    public async Task<ActionResult<PaginatedResponse<DutyShortInfo>>> UpdateDuty(int dutyId, DutyRequest request)
    {
        await dutiesService.UpdateDutyAsync(dutyId, request);
        return NoContent();
    }

    [HttpDelete("{dutyId}")]
    public async Task<ActionResult<PaginatedResponse<DutyShortInfo>>> DeleteDuty(int dutyId)
    {
        await dutiesService.DeleteDutyAsync(dutyId);
        return NoContent();
    }
}