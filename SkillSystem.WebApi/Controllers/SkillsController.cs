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

    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<ActionResult<int>> CreateSkill(CreateSkillRequest request)
    {
        var skillId = await skillsService.CreateSkillAsync(request);
        return Ok(skillId);
    }

    [HttpGet("{skillId}")]
    public async Task<ActionResult<SkillResponse>> GetSkillById(int skillId)
    {
        var skill = await skillsService.GetSkillByIdAsync(skillId);
        return Ok(skill);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<SkillShortInfo>>> FindSkills(
        [FromQuery] SearchSkillsRequest request
    )
    {
        var skills = await skillsService.FindSkillsAsync(request);
        return Ok(skills);
    }

    [HttpGet("{skillId}/sub-skills")]
    public async Task<ActionResult<IEnumerable<SkillShortInfo>>> GetSubSkills(int skillId)
    {
        var subSkills = await skillsService.GetSubSkillsAsync(skillId);
        return Ok(subSkills);
    }

    [HttpPut("{skillId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> UpdateSkill(int skillId, UpdateSkillRequest request)
    {
        await skillsService.UpdateSkillAsync(skillId, request);
        return NoContent();
    }

    [HttpPut("{skillId}/attach-to/{skillGroupId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> AttachSkillToGroup(int skillId, int skillGroupId)
    {
        await skillsService.AttachSkillToGroupAsync(skillId, skillGroupId);
        return NoContent();
    }

    [HttpDelete("{skillId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteSkill(int skillId)
    {
        await skillsService.DeleteSkillAsync(skillId);
        return NoContent();
    }
}