using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Services.Projects.Models;

namespace SkillSystem.Application.Services.Projects;

public interface IProjectRolesService
{
    Task<int> AddProjectRole(AddRoleToProjectRequest request);
    Task<ProjectRoleResponse> GetProjectRole(int projectRoleId);
    Task<ICollection<ProjectRoleShortInfo>> GetProjectRoles(int projectId);
    Task<PaginatedResponse<ProjectRoleResponse>> FindProjectRoles(ProjectRoleFilter query);
    Task<ICollection<EmployeeProjectRole>> FindEmployeeProjectRoles(Guid employeeId, int? projectId = null);
    Task SetEmployeeToProjectRole(SetEmployeeToProjectRoleRequest request);
    Task DeleteProjectRole(int projectRoleId);
}
