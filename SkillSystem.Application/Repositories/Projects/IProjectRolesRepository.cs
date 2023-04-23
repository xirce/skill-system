using SkillSystem.Application.Common.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Projects;

public interface IProjectRolesRepository
{
    Task AddProjectRole(ProjectRole projectRole);
    Task<ProjectRole?> FindProjectRole(int projectRoleId);
    Task<ProjectRole> GetProjectRole(int projectRoleId);
    Task<ICollection<ProjectRole>> GetProjectRoles(int projectId);
    PaginatedList<ProjectRole> FindProjectRoles(ProjectRoleFilter filter);
    Task<ICollection<ProjectRole>> FindEmployeeProjectRoles(Guid employeeId, int? projectId = null);
    void UpdateProjectRole(ProjectRole projectRole);
    void DeleteProjectRole(ProjectRole projectRole);
}
