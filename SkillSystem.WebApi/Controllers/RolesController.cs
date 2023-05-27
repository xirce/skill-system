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

    /// <summary>
    /// Создать роль.
    /// </summary>
    /// <returns>Идентификатор созданной роли</returns>
    [HttpPost]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<int> CreateRole(RoleRequest request)
    {
        return await rolesService.CreateRoleAsync(request);
    }

    /// <summary>
    /// Получить информацию о роли.
    /// </summary>
    /// <param name="roleId">Идентификатор роли</param>
    [HttpGet("{roleId}")]
    public async Task<ActionResult<RoleResponse>> GetRoleById(int roleId)
    {
        var role = await rolesService.GetRoleByIdAsync(roleId);
        return Ok(role);
    }

    /// <summary>
    /// Поиск ролей по названию.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<RoleShortInfo>>> FindRoles([FromQuery] SearchRolesRequest request)
    {
        var roles = await rolesService.FindRolesAsync(request);
        return Ok(roles);
    }

    /// <summary>
    /// Изменить информацию о роли.
    /// </summary>
    /// <param name="roleId">Идентификатор роли</param>
    [HttpPut("{roleId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> UpdateRole(int roleId, RoleRequest request)
    {
        await rolesService.UpdateRoleAsync(roleId, request);
        return NoContent();
    }

    /// <summary>
    /// Удалить роль.
    /// </summary>
    /// <param name="roleId">Идентификатор роли</param>
    [HttpDelete("{roleId}")]
    [Authorize(Roles = AuthRoleNames.Admin)]
    public async Task<IActionResult> DeleteRole(int roleId)
    {
        await rolesService.DeleteRoleAsync(roleId);
        return NoContent();
    }
}
