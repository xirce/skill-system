using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Skills;
using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/skills")]
public class SkillsController : BaseController
{
    private readonly ISkillsService skillsService;

    public SkillsController(ISkillsService skillsService)
    {
        this.skillsService = skillsService;
    }

    /// <summary>
    /// Создать скилл.
    /// </summary>
    /// <returns>Идентификатор созданного скилла</returns>
    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<ActionResult<int>> CreateSkill(CreateSkillRequest request)
    {
        var skillId = await skillsService.CreateSkillAsync(request);
        return Ok(skillId);
    }

    /// <summary>
    /// Получить информацию о скилле.
    /// </summary>
    /// <param name="skillId">Идентификатор скилла</param>
    [HttpGet("{skillId}")]
    public async Task<ActionResult<SkillResponse>> GetSkillById(int skillId)
    {
        var skill = await skillsService.GetSkillByIdAsync(skillId);
        return Ok(skill);
    }

    /// <summary>
    /// Поиск скиллов по названию.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<SkillShortInfo>>> FindSkills(
        [FromQuery] SearchSkillsRequest request)
    {
        var skills = await skillsService.FindSkillsAsync(request);
        return Ok(skills);
    }

    /// <summary>
    /// Получить подскиллы скилла.
    /// </summary>
    /// <param name="skillId">Идентификатор родительского скилла</param>
    /// <returns></returns>
    [HttpGet("{skillId}/sub-skills")]
    public async Task<ActionResult<IEnumerable<SkillShortInfo>>> GetSubSkills(int skillId)
    {
        var subSkills = await skillsService.GetSubSkillsAsync(skillId);
        return Ok(subSkills);
    }

    /// <summary>
    /// Изменить информацию о скилле.
    /// </summary>
    /// <param name="skillId">Идентификатор скилла</param>
    [HttpPut("{skillId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> UpdateSkill(int skillId, UpdateSkillRequest request)
    {
        await skillsService.UpdateSkillAsync(skillId, request);
        return NoContent();
    }

    /// <summary>
    /// Прикрепить скилл к родительскому скиллу.
    /// </summary>
    /// <param name="skillId">Идентификатор дочернего скилла</param>
    /// <param name="skillGroupId">Идентификатор родительского скилла</param>
    [HttpPut("{skillId}/attach-to")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> AttachSkillToGroup(int skillId, [FromQuery] int skillGroupId)
    {
        await skillsService.AttachSkillToGroupAsync(skillId, skillGroupId);
        return NoContent();
    }

    /// <summary>
    /// Удалить скилл.
    /// </summary>
    /// <param name="skillId">Идентификатор скилла</param>
    [HttpDelete("{skillId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteSkill(int skillId)
    {
        await skillsService.DeleteSkillAsync(skillId);
        return NoContent();
    }
}
