using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Services.Projects;
using SkillSystem.Application.Services.Projects.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/project-roles")]
public class ProjectRolesController : BaseController
{
    private readonly IProjectRolesService projectRolesService;

    public ProjectRolesController(IProjectRolesService projectRolesService)
    {
        this.projectRolesService = projectRolesService;
    }

    /// <summary>
    /// Добавить роль в проект (может быть пустой, а может быть с сотрудником).
    /// </summary>
    /// <returns>Идентификатор роли в проекте</returns>
    [HttpPost]
    public async Task<int> AddProjectRole(AddRoleToProjectRequest request)
    {
        return await projectRolesService.AddProjectRole(request);
    }

    /// <summary>
    /// Получить информацию о роли в проекте.
    /// </summary>
    /// <param name="projectRoleId">Идентификатор роли в проекте</param>
    /// <returns>Роль в проекте вместе с сотрудником, если он занимает данную роль</returns>
    [HttpGet("{projectRoleId}")]
    public async Task<ProjectRoleResponse> GetProjectRole(int projectRoleId)
    {
        return await projectRolesService.GetProjectRole(projectRoleId);
    }

    /// <summary>
    /// Получить роли проекта.
    /// </summary>
    /// <param name="projectId">Идентификатор проекта</param>
    [HttpGet("~/api/projects/{projectId}/roles")]
    public async Task<ICollection<ProjectRoleShortInfo>> GetProjectRoles(int projectId)
    {
        return await projectRolesService.GetProjectRoles(projectId);
    }

    /// <summary>
    /// Поиск с фильтрацией по ролям в проектах.
    /// </summary>
    [HttpPost("search")]
    public async Task<PaginatedResponse<ProjectRoleResponse>> FindProjectRoles(ProjectRoleFilter query)
    {
        return await projectRolesService.FindProjectRoles(query);
    }

    /// <summary>
    /// Назначить сотрудника на роль в проекте.
    /// </summary>
    /// <param name="projectRoleId">Идентифкатор роли в проекте</param>
    [HttpPut("{projectRoleId}")]
    public async Task SetEmployeeToProjectRole(int projectRoleId, SetEmployeeToProjectRoleRequest request)
    {
        request = request with { ProjectRoleId = projectRoleId };
        await projectRolesService.SetEmployeeToProjectRole(request);
    }

    /// <summary>
    /// Удалить роль из проекта.
    /// </summary>
    /// <param name="projectRoleId">Идентификатор роли в проекте</param>
    [HttpDelete("{projectRoleId}")]
    public async Task DeleteProjectRole(int projectRoleId)
    {
        await projectRolesService.DeleteProjectRole(projectRoleId);
    }
}
