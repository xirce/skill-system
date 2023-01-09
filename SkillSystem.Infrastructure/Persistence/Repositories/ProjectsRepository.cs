using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private readonly SkillSystemDbContext dbContext;

    public ProjectsRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateProjectAsync(Project project)
    {
        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();
        return project.Id;
    }

    public async Task<Project?> FindProjectByIdAsync(int projectId)
    {
        return await dbContext.Projects
            .Include(project => project.Roles)
            .ThenInclude(projectRole => projectRole.Role)
            .FirstOrDefaultAsync(project => project.Id == projectId);
    }

    public async Task<Project> GetProjectByIdAsync(int projectId)
    {
        return await FindProjectByIdAsync(projectId) ?? throw new EntityNotFoundException(nameof(Project), projectId);
    }

    public IQueryable<Project> FindProjects(ProjectFilter? filter = default)
    {
        var query = dbContext.Projects.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter?.Name))
            query = query.Where(project => project.Name.Contains(filter.Name));

        return query;
    }

    public async Task UpdateProjectAsync(Project project)
    {
        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteProjectAsync(Project project)
    {
        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();
    }
}