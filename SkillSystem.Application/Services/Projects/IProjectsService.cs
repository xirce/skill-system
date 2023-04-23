using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Services.Projects.Models;

namespace SkillSystem.Application.Services.Projects;

public interface IProjectsService
{
    Task<int> CreateProject(BaseProjectRequest request);
    Task<ProjectShortInfo> GetProjectById(int projectId);
    Task<PaginatedResponse<ProjectShortInfo>> FindProjects(ProjectFilter query);
    Task UpdateProject(UpdateProjectRequest request);
    Task DeleteProject(int projectId);
}
