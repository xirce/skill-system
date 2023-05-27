using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
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

    /// <summary>
    /// Создать обязанность.
    /// </summary>
    /// <returns>Идентификатор созданной обязанности</returns>
    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<ActionResult<int>> CreateDuty(DutyRequest request)
    {
        var dutyId = await dutiesService.CreateDutyAsync(request);
        return Ok(dutyId);
    }

    /// <summary>
    /// Получить информацию об обязанности.
    /// </summary>
    /// <param name="dutyId">Идентификатор обязанности</param>
    /// <returns>Обязанность</returns>
    [HttpGet("{dutyId}")]
    public async Task<ActionResult<DutyResponse>> GetDutyById(int dutyId)
    {
        var duty = await dutiesService.GetDutyByIdAsync(dutyId);
        return Ok(duty);
    }

    /// <summary>
    /// Поиск обязанностей по названию.
    /// </summary>
    /// <param name="query">Название обязанности</param>
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<DutyShortInfo>>> FindDuties(
        [FromQuery] PaginationQuery<DutyFilter> query)
    {
        var duties = await dutiesService.FindDutiesAsync(query);
        return Ok(duties);
    }

    /// <summary>
    /// Изменить информацию об обязанности.
    /// </summary>
    /// <param name="dutyId">Идентификатор обязанности</param>
    [HttpPut("{dutyId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> UpdateDuty(int dutyId, DutyRequest request)
    {
        await dutiesService.UpdateDutyAsync(dutyId, request);
        return NoContent();
    }

    /// <summary>
    /// Удалить обязанность.
    /// </summary>
    /// <param name="dutyId">Идентификатор обязанности</param>
    [HttpDelete("{dutyId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteDuty(int dutyId)
    {
        await dutiesService.DeleteDutyAsync(dutyId);
        return NoContent();
    }
}
