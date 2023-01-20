using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Roles;

public interface IRolesRepository
{
    Task<int> CreateRoleAsync(Role role);
    Task<Role?> FindRoleByIdAsync(int roleId);
    Task<Role> GetRoleByIdAsync(int roleId);
    IQueryable<Role> FindRoles(string? title = default);
    Task<ICollection<Grade>> GetRoleGradesAsync(int roleId, bool includeSkills = false);
    Task UpdateRoleAsync(Role role);
    Task<int> AddGradeAfterAsync(int roleId, Grade grade, int? prevGradeId);
    Task InsertGradeAfterAsync(int roleId, int gradeId, int? prevGradeId);
    Task DeleteGradeAsync(int roleId, int gradeId);
    Task DeleteRoleAsync(Role role);
}