using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Services.Projects.Models;

namespace SkillSystem.Application.Services.Projects;

public interface IProjectRolesService
{
    Task<int> AddProjectRoleAsync(ProjectRoleRequest request);
    Task<ProjectRoleResponse> GetProjectRoleAsync(int projectRoleId);
    Task<ICollection<ProjectRoleResponse>> GetProjectRolesAsync(int projectId);
    Task<PaginatedResponse<ProjectRoleResponse>> FindProjectRolesAsync(PaginationQuery<ProjectRoleFilter> query);
    Task<ICollection<ProjectRoleResponse>> GetEmployeeProjectRolesAsync(string employeeId);
    Task<ICollection<ProjectRoleResponse>> FindRolesInProjectAsync(string employeeId, int projectId);
    Task SetEmployeeToProjectRoleAsync(int projectRoleId, string? employeeId);
    Task DeleteProjectRoleAsync(int projectRoleId);
}