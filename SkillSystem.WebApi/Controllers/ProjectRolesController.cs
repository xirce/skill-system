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

    [HttpPost]
    public async Task<int> AddProjectRole(AddRoleToProjectRequest request)
    {
        return await projectRolesService.AddProjectRole(request);
    }

    [HttpGet("{projectRoleId}")]
    public async Task<ProjectRoleResponse> GetProjectRole(int projectRoleId)
    {
        return await projectRolesService.GetProjectRole(projectRoleId);
    }

    [HttpGet("~/api/projects/{projectId}/roles")]
    public async Task<ICollection<ProjectRoleShortInfo>> GetProjectRoles(int projectId)
    {
        return await projectRolesService.GetProjectRoles(projectId);
    }

    [HttpPost("search")]
    public async Task<PaginatedResponse<ProjectRoleResponse>> FindProjectRoles(ProjectRoleFilter query)
    {
        return await projectRolesService.FindProjectRoles(query);
    }


    [HttpPut("{projectRoleId}")]
    public async Task SetEmployeeToProjectRole(int projectRoleId, SetEmployeeToProjectRoleRequest request)
    {
        request = request with { ProjectRoleId = projectRoleId };
        await projectRolesService.SetEmployeeToProjectRole(request);
    }

    [HttpDelete("{projectRoleId}")]
    public async Task DeleteProjectRole(int projectRoleId)
    {
        await projectRolesService.DeleteProjectRole(projectRoleId);
    }
}
