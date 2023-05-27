using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Services.Duties.Models;
using SkillSystem.Application.Services.Positions;
using SkillSystem.WebApi.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/positions/{positionId}/duties")]
public class PositionDutiesController : BaseController
{
    private readonly IPositionsService positionsService;

    public PositionDutiesController(IPositionsService positionsService)
    {
        this.positionsService = positionsService;
    }

    /// <summary>
    /// Получить обязанности должности.
    /// </summary>
    /// <param name="positionId">Идентификатор должности</param>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DutyShortInfo>>> GetPositionDuties(int positionId)
    {
        var duties = await positionsService.GetPositionDutiesAsync(positionId);
        return Ok(duties);
    }

    /// <summary>
    /// Добавить обязанность к должности.
    /// </summary>
    /// <param name="positionId">Идентификатор должности</param>
    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> AddPositionDuty(int positionId, PositionDutyRequest dutyRequest)
    {
        await positionsService.AddPositionDutyAsync(positionId, dutyRequest.DutyId);
        return Ok(dutyRequest.DutyId);
    }

    /// <summary>
    /// Удалить обязанность из должности.
    /// </summary>
    /// <param name="positionId">Идентификатор должности</param>
    /// <param name="dutyId">Идентификатор обязанности</param>
    [HttpDelete("{dutyId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeletePositionDuty(int positionId, int dutyId)
    {
        await positionsService.DeletePositionDutyAsync(positionId, dutyId);
        return NoContent();
    }
}
