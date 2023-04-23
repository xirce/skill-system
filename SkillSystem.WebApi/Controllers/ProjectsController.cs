using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Services.Projects;
using SkillSystem.Application.Services.Projects.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/projects")]
public class ProjectsController : BaseController
{
    private readonly IProjectsService projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        this.projectsService = projectsService;
    }

    [HttpPost]
    public async Task<int> CreateProject(BaseProjectRequest request)
    {
        return await projectsService.CreateProject(request);
    }

    [HttpGet("{projectId}")]
    public async Task<ProjectShortInfo> GetProject(int projectId)
    {
        return await projectsService.GetProjectById(projectId);
    }

    [HttpGet]
    public async Task<PaginatedResponse<ProjectShortInfo>> FindProjects([FromQuery] ProjectFilter query)
    {
        return await projectsService.FindProjects(query);
    }

    [HttpPut("{projectId}")]
    public async Task UpdateProject(int projectId, UpdateProjectRequest request)
    {
        request = request with { ProjectId = projectId };
        await projectsService.UpdateProject(request);
    }

    [HttpDelete("{projectId}")]
    public async Task DeleteProject(int projectId)
    {
        await projectsService.DeleteProject(projectId);
    }
}
