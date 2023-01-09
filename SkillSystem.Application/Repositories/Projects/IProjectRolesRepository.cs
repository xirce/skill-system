using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Projects;

public interface IProjectRolesRepository
{
    Task<int> AddProjectRoleAsync(ProjectRole projectRole);
    Task<ProjectRole?> FindProjectRoleAsync(int projectRoleId);
    Task<ProjectRole> GetProjectRoleAsync(int projectRoleId);
    Task<ICollection<ProjectRole>> GetProjectRolesAsync(int projectId);
    IQueryable<ProjectRole> FindProjectRoles(ProjectRoleFilter? filter = default);
    Task<ICollection<ProjectRole>> GetEmployeeProjectRolesAsync(string employeeId);
    Task<ICollection<ProjectRole>> FindRolesInProjectAsync(string employeeId, int projectId);
    Task UpdateProjectRoleAsync(ProjectRole projectRole);
    Task DeleteProjectRoleAsync(ProjectRole projectRole);
}