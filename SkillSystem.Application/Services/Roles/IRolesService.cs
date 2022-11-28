using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Roles.Models;

namespace SkillSystem.Application.Services.Roles;

public interface IRolesService
{
    Task<int> CreateRoleAsync(RoleRequest request);
    Task<RoleResponse> GetRoleByIdAsync(int roleId);
    Task<PaginatedResponse<RoleResponse>> FindRolesAsync(SearchRolesRequest request);
    Task UpdateRoleAsync(int roleId, RoleRequest request);
    Task DeleteRoleAsync(int roleId);
}