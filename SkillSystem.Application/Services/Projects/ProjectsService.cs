using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Services.Projects.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IProjectsRepository projectsRepository;

    public ProjectsService(IProjectsRepository projectsRepository)
    {
        this.projectsRepository = projectsRepository;
    }

    public async Task<int> CreateProjectAsync(ProjectRequest request)
    {
        var project = request.Adapt<Project>();
        return await projectsRepository.CreateProjectAsync(project);
    }

    public async Task<ProjectResponse> GetProjectByIdAsync(int projectId)
    {
        var project = await projectsRepository.GetProjectByIdAsync(projectId);
        return project.Adapt<ProjectResponse>();
    }

    public Task<PaginatedResponse<ProjectShortInfo>> FindProjectsAsync(PaginationQuery<ProjectFilter> query)
    {
        var projects = projectsRepository.FindProjects(query.Filter);

        var paginatedProjects = projects
            .ProjectToType<ProjectShortInfo>()
            .ToPaginatedList(query)
            .ToResponse();

        return Task.FromResult(paginatedProjects);
    }

    public async Task UpdateProjectAsync(int projectId, ProjectRequest request)
    {
        var project = await projectsRepository.GetProjectByIdAsync(projectId);

        request.Adapt(project);

        await projectsRepository.UpdateProjectAsync(project);
    }

    public async Task DeleteProjectAsync(int projectId)
    {
        var project = await projectsRepository.FindProjectByIdAsync(projectId);
        if (project is not null)
            await projectsRepository.DeleteProjectAsync(project);
    }
}