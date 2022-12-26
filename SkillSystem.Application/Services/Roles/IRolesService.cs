using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Grades.Models;
using SkillSystem.Application.Services.Roles.Models;

namespace SkillSystem.Application.Services.Roles;

public interface IRolesService
{
    Task<int> CreateRoleAsync(RoleRequest request);
    Task<RoleResponse> GetRoleByIdAsync(int roleId);
    Task<PaginatedResponse<RoleShortInfo>> FindRolesAsync(SearchRolesRequest request);
    Task<ICollection<GradeShortInfo>> GetRoleGradesAsync(int roleId);
    Task<ICollection<GradeWithSkills>> GetRoleGradesWithSkillsAsync(int roleId);
    Task<int> AddGradeAsync(int roleId, GradeRequest request, int? prevGradeId);
    Task InsertGradeAfterAsync(int roleId, int gradeId, int? prevGradeId);
    Task UpdateRoleAsync(int roleId, RoleRequest request);
    Task DeleteGradeAsync(int roleId, int gradeId);
    Task DeleteRoleAsync(int roleId);
}