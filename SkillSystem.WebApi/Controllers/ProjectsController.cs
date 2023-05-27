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

    /// <summary>
    /// Создать проект.
    /// </summary>
    /// <returns>Идентификатор созданного проекта</returns>
    [HttpPost]
    public async Task<int> CreateProject(BaseProjectRequest request)
    {
        return await projectsService.CreateProject(request);
    }

    /// <summary>
    /// Получить информацию о проекте.
    /// </summary>
    /// <param name="projectId">Идентификатор проекта</param>
    /// <returns></returns>
    [HttpGet("{projectId}")]
    public async Task<ProjectShortInfo> GetProject(int projectId)
    {
        return await projectsService.GetProjectById(projectId);
    }

    /// <summary>
    /// Поиск проектов по названию.
    /// </summary>
    [HttpGet]
    public async Task<PaginatedResponse<ProjectShortInfo>> FindProjects([FromQuery] ProjectFilter query)
    {
        return await projectsService.FindProjects(query);
    }

    /// <summary>
    /// Изменить информацию о проекте.
    /// </summary>
    /// <param name="projectId">Идентификатор проекта</param>
    [HttpPut("{projectId}")]
    public async Task UpdateProject(int projectId, UpdateProjectRequest request)
    {
        request = request with { ProjectId = projectId };
        await projectsService.UpdateProject(request);
    }

    /// <summary>
    /// Удалить проект.
    /// </summary>
    /// <param name="projectId">Идентификатор проекта</param>
    [HttpDelete("{projectId}")]
    public async Task DeleteProject(int projectId)
    {
        await projectsService.DeleteProject(projectId);
    }
}
