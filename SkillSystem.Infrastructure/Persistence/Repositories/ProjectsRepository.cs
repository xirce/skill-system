using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models;
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

    public async Task CreateProject(Project project)
    {
        await dbContext.Projects.AddAsync(project);
    }

    public async Task<Project?> FindProjectById(int projectId)
    {
        return await dbContext.Projects.FirstOrDefaultAsync(project => project.Id == projectId);
    }

    public async Task<Project> GetProjectById(int projectId)
    {
        return await FindProjectById(projectId) ?? throw new EntityNotFoundException(nameof(Project), projectId);
    }

    public PaginatedList<Project> FindProjects(ProjectFilter filter)
    {
        var query = dbContext.Projects.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(project => project.Name.Contains(filter.Name));

        return query
            .OrderBy(project => project.Id)
            .ToPaginatedList(filter);
    }

    public void UpdateProject(Project project)
    {
        dbContext.Projects.Update(project);
    }

    public void DeleteProject(Project project)
    {
        dbContext.Projects.Remove(project);
    }
}
