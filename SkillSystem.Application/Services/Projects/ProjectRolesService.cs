using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Employees;
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
    private readonly IEmployeesRepository employeesRepository;
    private readonly IUnitOfWork unitOfWork;

    public ProjectRolesService(
        IProjectRolesRepository projectRolesRepository,
        IProjectsRepository projectsRepository,
        IRolesRepository rolesRepository,
        IEmployeesRepository employeesRepository,
        IUnitOfWork unitOfWork)
    {
        this.projectRolesRepository = projectRolesRepository;
        this.projectsRepository = projectsRepository;
        this.rolesRepository = rolesRepository;
        this.employeesRepository = employeesRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<int> AddProjectRole(AddRoleToProjectRequest request)
    {
        await projectsRepository.GetProjectById(request.ProjectId);
        await rolesRepository.GetRoleByIdAsync(request.RoleId);

        if (request.EmployeeId.HasValue)
        {
            await employeesRepository.GetEmployeeById(request.EmployeeId.Value);
            await EnsureEmployeeNotInProjectRole(request.EmployeeId.Value, request.ProjectId, request.RoleId);
        }

        var projectRole = request.Adapt<ProjectRole>();

        await projectRolesRepository.AddProjectRole(projectRole);
        await unitOfWork.SaveChangesAsync();
        return projectRole.Id;
    }

    public async Task<ProjectRoleResponse> GetProjectRole(int projectRoleId)
    {
        var projectRole = await projectRolesRepository.GetProjectRole(projectRoleId);
        return projectRole.Adapt<ProjectRoleResponse>();
    }

    public async Task<ICollection<ProjectRoleShortInfo>> GetProjectRoles(int projectId)
    {
        var project = await projectsRepository.GetProjectById(projectId);
        var projectRoles = await projectRolesRepository.GetProjectRoles(project.Id);
        return projectRoles.Adapt<ICollection<ProjectRoleShortInfo>>();
    }

    public Task<PaginatedResponse<ProjectRoleResponse>> FindProjectRoles(ProjectRoleFilter query)
    {
        var projectRoles = projectRolesRepository.FindProjectRoles(query);
        var projectRoleViews = projectRoles.Adapt<IReadOnlyCollection<ProjectRoleResponse>>();
        var response = PaginatedList.Create(projectRoleViews, projectRoles.Offset, projectRoles.TotalCount)
            .ToResponse();
        return Task.FromResult(response);
    }

    public async Task<ICollection<EmployeeProjectRole>> FindEmployeeProjectRoles(Guid employeeId, int? projectId = null)
    {
        var projectRoles = await projectRolesRepository.FindEmployeeProjectRoles(employeeId, projectId);
        return projectRoles.Adapt<ICollection<EmployeeProjectRole>>();
    }

    public async Task SetEmployeeToProjectRole(SetEmployeeToProjectRoleRequest request)
    {
        var projectRole = await projectRolesRepository.GetProjectRole(request.ProjectRoleId);
        if (request.EmployeeId.HasValue)
            await employeesRepository.GetEmployeeById(request.EmployeeId.Value);

        projectRole.EmployeeId = request.EmployeeId;
        projectRolesRepository.UpdateProjectRole(projectRole);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProjectRole(int projectRoleId)
    {
        var projectRole = await projectRolesRepository.FindProjectRole(projectRoleId);
        if (projectRole is null)
            return;

        projectRolesRepository.DeleteProjectRole(projectRole);
        await unitOfWork.SaveChangesAsync();
    }

    private async Task EnsureEmployeeNotInProjectRole(Guid employeeId, int projectId, int roleId)
    {
        var presentProjectRole = await FindEmployeeProjectRole(employeeId, projectId, roleId);
        if (presentProjectRole is not null)
            throw new InvalidOperationException("Employee already in role");
    }

    private async Task<ProjectRole?> FindEmployeeProjectRole(Guid employeeId, int projectId, int roleId)
    {
        var projectRoles = await projectRolesRepository.FindEmployeeProjectRoles(employeeId, projectId);
        return projectRoles.FirstOrDefault(projectRole => projectRole.RoleId == roleId);
    }
}
