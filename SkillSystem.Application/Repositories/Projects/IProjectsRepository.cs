using SkillSystem.Application.Common.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Projects;

public interface IProjectsRepository
{
    Task CreateProject(Project project);
    Task<Project?> FindProjectById(int projectId);
    Task<Project> GetProjectById(int projectId);
    PaginatedList<Project> FindProjects(ProjectFilter filter);
    void UpdateProject(Project project);
    void DeleteProject(Project project);
}
