using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Roles;

public interface IRolesRepository
{
    Task<int> CreateRoleAsync(Role role);
    Task<Role?> FindRoleByIdAsync(int roleId);
    IQueryable<Role> FindRoles(string? title = default);
    Task UpdateRoleAsync(Role role);
    Task DeleteRoleAsync(int roleId);
}