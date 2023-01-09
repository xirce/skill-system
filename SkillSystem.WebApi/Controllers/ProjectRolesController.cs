using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Common.Models.Requests;
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
    public async Task<ActionResult<int>> AddProjectRoleAsync(ProjectRoleRequest request)
    {
        var projectRoleId = await projectRolesService.AddProjectRoleAsync(request);
        return Ok(projectRoleId);
    }

    [HttpGet("{projectRoleId}")]
    public async Task<ActionResult<ProjectRoleResponse>> GetProjectRoleAsync(int projectRoleId)
    {
        var projectRole = await projectRolesService.GetProjectRoleAsync(projectRoleId);
        return Ok(projectRole);
    }

    [HttpGet("~/api/projects/{projectId}/roles")]
    public async Task<ActionResult<ICollection<ProjectRoleResponse>>> GetProjectRolesAsync(int projectId)
    {
        var projectRoles = await projectRolesService.GetProjectRolesAsync(projectId);
        return Ok(projectRoles);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<ProjectRoleResponse>>> FindProjectRolesAsync(
        [FromQuery] PaginationQuery<ProjectRoleFilter> query
    )
    {
        var projectRoles = await projectRolesService.FindProjectRolesAsync(query);
        return Ok(projectRoles);
    }

    [HttpGet("~/api/employees/{employeeId}/project-roles")]
    public async Task<ActionResult<ICollection<ProjectRoleResponse>>> GetEmployeeProjectRolesAsync(string employeeId)
    {
        var projectRoles = await projectRolesService.GetEmployeeProjectRolesAsync(employeeId);
        return Ok(projectRoles);
    }

    [HttpGet("~/api/employees/{employeeId}/projects/{projectId}/roles")]
    public async Task<ActionResult<ICollection<ProjectRoleResponse>>> FindRolesInProjectAsync(
        string employeeId,
        int projectId
    )
    {
        var projectRoles = await projectRolesService.FindRolesInProjectAsync(employeeId, projectId);
        return Ok(projectRoles);
    }

    [HttpPut("{projectRoleId}")]
    public async Task<IActionResult> SetEmployeeToProjectRoleAsync(int projectRoleId, string? employeeId)
    {
        await projectRolesService.SetEmployeeToProjectRoleAsync(projectRoleId, employeeId);
        return NoContent();
    }

    [HttpDelete("{projectRoleId}")]
    public async Task<IActionResult> DeleteProjectRoleAsync(int projectRoleId)
    {
        await projectRolesService.DeleteProjectRoleAsync(projectRoleId);
        return NoContent();
    }
}