using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Application.Services.Projects.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Projects;

public class ProjectRolesService : IProjectRolesService
{
    private readonly IProjectRolesRepository projectRolesRepository;
    private readonly IProjectsRepository projectsRepository;
    private readonly IRolesRepository rolesRepository;

    public ProjectRolesService(
        IProjectRolesRepository projectRolesRepository,
        IProjectsRepository projectsRepository,
        IRolesRepository rolesRepository
    )
    {
        this.projectRolesRepository = projectRolesRepository;
        this.projectsRepository = projectsRepository;
        this.rolesRepository = rolesRepository;
    }

    public async Task<int> AddProjectRoleAsync(ProjectRoleRequest request)
    {
        await projectsRepository.GetProjectByIdAsync(request.ProjectId);
        await rolesRepository.GetRoleByIdAsync(request.RoleId);

        var projectRole = request.Adapt<ProjectRole>();

        return await projectRolesRepository.AddProjectRoleAsync(projectRole);
    }

    public async Task<ProjectRoleResponse> GetProjectRoleAsync(int projectRoleId)
    {
        var projectRole = await projectRolesRepository.GetProjectRoleAsync(projectRoleId);
        return projectRole.Adapt<ProjectRoleResponse>();
    }

    public async Task<ICollection<ProjectRoleResponse>> GetProjectRolesAsync(int projectId)
    {
        var project = await projectsRepository.GetProjectByIdAsync(projectId);
        var projectRoles = await projectRolesRepository.GetProjectRolesAsync(project.Id);
        return projectRoles.Adapt<ICollection<ProjectRoleResponse>>();
    }

    public Task<PaginatedResponse<ProjectRoleResponse>> FindProjectRolesAsync(PaginationQuery<ProjectRoleFilter> query)
    {
        var projectRoles = projectRolesRepository.FindProjectRoles(query.Filter);

        var paginatedProjectRoles = projectRoles
            .ProjectToType<ProjectRoleResponse>()
            .ToPaginatedList(query)
            .ToResponse();

        return Task.FromResult(paginatedProjectRoles);
    }

    public async Task<ICollection<ProjectRoleResponse>> GetEmployeeProjectRolesAsync(string employeeId)
    {
        var projectRoles = await projectRolesRepository.GetEmployeeProjectRolesAsync(employeeId);
        return projectRoles.Adapt<ICollection<ProjectRoleResponse>>();
    }

    public async Task<ICollection<ProjectRoleResponse>> FindRolesInProjectAsync(string employeeId, int projectId)
    {
        var projectRoles = await projectRolesRepository.FindRolesInProjectAsync(employeeId, projectId);
        return projectRoles.Adapt<ICollection<ProjectRoleResponse>>();
    }

    public async Task SetEmployeeToProjectRoleAsync(int projectRoleId, string? employeeId)
    {
        var projectRole = await projectRolesRepository.GetProjectRoleAsync(projectRoleId);
        projectRole.EmployeeId = employeeId;
        await projectRolesRepository.UpdateProjectRoleAsync(projectRole);
    }

    public async Task DeleteProjectRoleAsync(int projectRoleId)
    {
        var projectRole = await projectRolesRepository.FindProjectRoleAsync(projectRoleId);
        if (projectRole is not null)
            await projectRolesRepository.DeleteProjectRoleAsync(projectRole);
    }
}