using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models;
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

    public async Task AddProjectRole(ProjectRole projectRole)
    {
        await dbContext.ProjectRoles.AddAsync(projectRole);
    }

    public async Task<ProjectRole?> FindProjectRole(int projectRoleId)
    {
        return await dbContext.ProjectRoles
            .Include(projectRole => projectRole.Project)
            .Include(projectRole => projectRole.Role)
            .Include(projectRole => projectRole.Employee)
            .FirstOrDefaultAsync(projectRole => projectRole.Id == projectRoleId);
    }

    public async Task<ProjectRole> GetProjectRole(int projectRoleId)
    {
        return await FindProjectRole(projectRoleId)
               ?? throw new EntityNotFoundException(nameof(ProjectRole), projectRoleId);
    }

    public async Task<ICollection<ProjectRole>> GetProjectRoles(int projectId)
    {
        return await dbContext.ProjectRoles
            .Include(projectRole => projectRole.Role)
            .Include(projectRole => projectRole.Employee)
            .Where(projectRole => projectRole.ProjectId == projectId)
            .ToListAsync();
    }

    public PaginatedList<ProjectRole> FindProjectRoles(ProjectRoleFilter filter)
    {
        var query = dbContext.ProjectRoles
            .Include(projectRole => projectRole.Project)
            .Include(projectRole => projectRole.Role)
            .Include(projectRole => projectRole.Employee)
            .AsNoTracking();

        if (filter.ProjectIds?.Length > 0)
            query = query.Where(projectRole => filter.ProjectIds.Contains(projectRole.ProjectId));

        if (filter.RoleIds?.Length > 0)
            query = query.Where(projectRole => filter.RoleIds.Contains(projectRole.RoleId));

        if (filter.IsFree.HasValue)
            query = query.Where(projectRole => projectRole.EmployeeId == null == filter.IsFree);

        return query
            .OrderBy(projectRole => projectRole.Id)
            .ToPaginatedList(filter);
    }
    public async Task<ICollection<ProjectRole>> FindEmployeeProjectRoles(Guid employeeId, int? projectId = null)
    {
        var query = dbContext.ProjectRoles
            .AsNoTracking()
            .Include(projectRole => projectRole.Project)
            .Include(projectRole => projectRole.Role)
            .Where(projectRole => projectRole.EmployeeId == employeeId);

        if (projectId.HasValue)
            query = query.Where(projectRole => projectRole.ProjectId == projectId);

        return await query.ToListAsync();
    }

    public void UpdateProjectRole(ProjectRole projectRole)
    {
        dbContext.ProjectRoles.Update(projectRole);
    }

    public void DeleteProjectRole(ProjectRole projectRole)
    {
        dbContext.ProjectRoles.Remove(projectRole);
    }
}
