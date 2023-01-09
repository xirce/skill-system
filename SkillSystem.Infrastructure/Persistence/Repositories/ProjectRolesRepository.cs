using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class ProjectRolesRepository : IProjectRolesRepository
{
    private readonly SkillSystemDbContext dbContext;

    public ProjectRolesRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> AddProjectRoleAsync(ProjectRole projectRole)
    {
        if (!projectRole.IsFree)
        {
            var presentProjectRole = await FindProjectRoleAsync(
                projectRole.EmployeeId, projectRole.ProjectId, projectRole.RoleId
            );
            if (presentProjectRole is not null)
                return presentProjectRole.Id;
        }

        await dbContext.ProjectRoles.AddAsync(projectRole);
        await dbContext.SaveChangesAsync();
        return projectRole.Id;
    }

    public async Task<ProjectRole?> FindProjectRoleAsync(int projectRoleId)
    {
        return await dbContext.ProjectRoles
            .Include(projectRole => projectRole.Role)
            .FirstOrDefaultAsync(projectRole => projectRole.Id == projectRoleId);
    }

    public async Task<ProjectRole> GetProjectRoleAsync(int projectRoleId)
    {
        return await FindProjectRoleAsync(projectRoleId)
               ?? throw new EntityNotFoundException(nameof(ProjectRole), projectRoleId);
    }

    public async Task<ICollection<ProjectRole>> GetProjectRolesAsync(int projectId)
    {
        return await dbContext.ProjectRoles
            .Include(projectRole => projectRole.Role)
            .Where(projectRole => projectRole.ProjectId == projectId)
            .ToListAsync();
    }

    public IQueryable<ProjectRole> FindProjectRoles(ProjectRoleFilter filter)
    {
        var query = dbContext.ProjectRoles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.RoleName))
            query = query.Where(projectRole => projectRole.Role.Title.Contains(filter.RoleName));

        if (filter.IsFree.HasValue)
            query = query.Where(projectRole => projectRole.IsFree);

        return query;
    }

    public async Task<ICollection<ProjectRole>> GetEmployeeProjectRolesAsync(string employeeId)
    {
        return await FindEmployeeProjectRoles(employeeId).ToListAsync();
    }

    public async Task<ICollection<ProjectRole>> FindRolesInProjectAsync(string employeeId, int projectId)
    {
        return await FindEmployeeProjectRoles(employeeId)
            .Where(projectRole => projectRole.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task UpdateProjectRoleAsync(ProjectRole projectRole)
    {
        dbContext.ProjectRoles.Update(projectRole);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteProjectRoleAsync(ProjectRole projectRole)
    {
        dbContext.ProjectRoles.Remove(projectRole);
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<ProjectRole> FindEmployeeProjectRoles(string employeeId)
    {
        return dbContext.ProjectRoles.Where(projectRole => projectRole.EmployeeId == employeeId);
    }

    private async Task<ProjectRole?> FindProjectRoleAsync(string employeeId, int projectId, int roleId)
    {
        return await dbContext.ProjectRoles
            .FirstOrDefaultAsync(
                projectRole => projectRole.EmployeeId == employeeId
                               && projectRole.ProjectId == projectId
                               && projectRole.RoleId == roleId
            );
    }
}