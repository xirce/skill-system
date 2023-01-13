using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Roles;
using SkillSystem.Application.Services.Roles.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/roles")]
public class RolesController : BaseController
{
    private readonly IRolesService rolesService;

    public RolesController(IRolesService rolesService)
    {
        this.rolesService = rolesService;
    }

    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<int> CreateRole(RoleRequest request)
    {
        return await rolesService.CreateRoleAsync(request);
    }

    [HttpGet("{roleId}")]
    public async Task<ActionResult<RoleResponse>> GetRoleById(int roleId)
    {
        var role = await rolesService.GetRoleByIdAsync(roleId);
        return Ok(role);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<RoleShortInfo>>> FindRoles([FromQuery] SearchRolesRequest request)
    {
        var roles = await rolesService.FindRolesAsync(request);
        return Ok(roles);
    }

    [HttpPut("{roleId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> UpdateRole(int roleId, RoleRequest request)
    {
        await rolesService.UpdateRoleAsync(roleId, request);
        return NoContent();
    }

    [HttpDelete("{roleId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteRole(int roleId)
    {
        await rolesService.DeleteRoleAsync(roleId);
        return NoContent();
    }
}