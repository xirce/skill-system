using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Projects;

public interface IProjectsRepository
{
    Task<int> CreateProjectAsync(Project project);
    Task<Project?> FindProjectByIdAsync(int projectId);
    Task<Project> GetProjectByIdAsync(int projectId);
    IQueryable<Project> FindProjects(ProjectFilter? filter = default);
    Task UpdateProjectAsync(Project project);
    Task DeleteProjectAsync(Project project);
}