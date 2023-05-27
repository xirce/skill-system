using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Positions.Filters;
using SkillSystem.Application.Services.Positions;
using SkillSystem.Application.Services.Positions.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/positions")]
public class PositionsController : BaseController
{
    private readonly IPositionsService positionsService;

    public PositionsController(IPositionsService positionsService)
    {
        this.positionsService = positionsService;
    }

    /// <summary>
    /// Создать должность.
    /// </summary>
    /// <returns>Идентификатор созданной должности</returns>
    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<ActionResult<int>> CreatePosition(PositionRequest request)
    {
        var positionId = await positionsService.CreatePositionAsync(request);
        return Ok(positionId);
    }

    /// <summary>
    /// Получить информацию о должности.
    /// </summary>
    /// <param name="positionId">Идентификатор должности</param>
    [HttpGet("{positionId}")]
    public async Task<ActionResult<PositionResponse>> GetPosition(int positionId)
    {
        var position = await positionsService.GetPositionByIdAsync(positionId);
        return Ok(position);
    }

    /// <summary>
    /// Поиск должностей по названию.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<PositionResponse>>> FindPositions(
        [FromQuery] PaginationQuery<PositionFilter> query)
    {
        var positions = await positionsService.FindPositionsAsync(query);
        return Ok(positions);
    }

    /// <summary>
    /// Изменить информацию о должности.
    /// </summary>
    /// <param name="positionId">Идентификатор должности</param>
    [HttpPut("{positionId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> UpdatePosition(int positionId, PositionRequest request)
    {
        await positionsService.UpdatePositionAsync(positionId, request);
        return NoContent();
    }

    /// <summary>
    /// Удалить должность.
    /// </summary>
    /// <param name="positionId">Идентификатор должности</param>
    [HttpDelete("{positionId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeletePosition(int positionId)
    {
        await positionsService.DeletePositionAsync(positionId);
        return NoContent();
    }
}
