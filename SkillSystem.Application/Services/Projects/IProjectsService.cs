using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Services.Projects.Models;

namespace SkillSystem.Application.Services.Projects;

public interface IProjectsService
{
    Task<int> CreateProjectAsync(ProjectRequest request);
    Task<ProjectResponse> GetProjectByIdAsync(int projectId);
    Task<PaginatedResponse<ProjectShortInfo>> FindProjectsAsync(PaginationQuery<ProjectFilter> query);
    Task UpdateProjectAsync(int projectId, ProjectRequest request);
    Task DeleteProjectAsync(int projectId);
}