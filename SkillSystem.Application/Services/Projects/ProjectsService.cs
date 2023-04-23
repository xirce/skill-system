using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Services.Projects.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IProjectsRepository projectsRepository;
    private readonly IUnitOfWork unitOfWork;

    public ProjectsService(IProjectsRepository projectsRepository, IUnitOfWork unitOfWork)
    {
        this.projectsRepository = projectsRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<int> CreateProject(BaseProjectRequest request)
    {
        var project = request.Adapt<Project>();

        await projectsRepository.CreateProject(project);
        await unitOfWork.SaveChangesAsync();

        return project.Id;
    }

    public async Task<ProjectShortInfo> GetProjectById(int projectId)
    {
        var project = await projectsRepository.GetProjectById(projectId);
        return project.Adapt<ProjectShortInfo>();
    }

    public Task<PaginatedResponse<ProjectShortInfo>> FindProjects(ProjectFilter query)
    {
        var projects = projectsRepository.FindProjects(query);
        var projectViews = projects.Adapt<IReadOnlyCollection<ProjectShortInfo>>();
        var response = PaginatedList.Create(projectViews, projects.Offset, projects.TotalCount)
            .ToResponse();
        return Task.FromResult(response);
    }

    public async Task UpdateProject(UpdateProjectRequest request)
    {
        var project = await projectsRepository.GetProjectById(request.ProjectId);

        project.Name = request.Name;

        projectsRepository.UpdateProject(project);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProject(int projectId)
    {
        var project = await projectsRepository.FindProjectById(projectId);
        if (project is null)
            return;

        projectsRepository.DeleteProject(project);
        await unitOfWork.SaveChangesAsync();
    }
}
