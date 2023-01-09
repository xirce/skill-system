using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Common.Models.Requests;
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
    public async Task<ActionResult<int>> CreateProject(ProjectRequest request)
    {
        var projectId = await projectsService.CreateProjectAsync(request);
        return Ok(projectId);
    }

    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectResponse>> GetProject(int projectId)
    {
        var project = await projectsService.GetProjectByIdAsync(projectId);
        return Ok(project);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<ProjectShortInfo>>> FindProjects(
        [FromQuery] PaginationQuery<ProjectFilter> query
    )
    {
        var projects = await projectsService.FindProjectsAsync(query);
        return Ok(projects);
    }

    [HttpPut("{projectId}")]
    public async Task<IActionResult> UpdateProject(int projectId, ProjectRequest request)
    {
        await projectsService.UpdateProjectAsync(projectId, request);
        return NoContent();
    }

    [HttpDelete("{projectId}")]
    public async Task<IActionResult> DeleteProject(int projectId)
    {
        await projectsService.DeleteProjectAsync(projectId);
        return NoContent();
    }
}