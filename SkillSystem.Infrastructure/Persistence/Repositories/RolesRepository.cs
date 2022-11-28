using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly SkillSystemDbContext dbContext;

    public RolesRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateRoleAsync(Role role)
    {
        await dbContext.Roles.AddAsync(role);
        await dbContext.SaveChangesAsync();
        return role.Id;
    }

    public async Task<Role?> FindRoleByIdAsync(int roleId)
    {
        return await dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Id == roleId);
    }

    public IQueryable<Role> FindRoles(string? title = default)
    {
        var roles = dbContext.Roles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(title))
            roles = roles.Where(role => role.Title.Contains(title));

        return roles.OrderBy(role => role.Id);
    }

    public async Task UpdateRoleAsync(Role role)
    {
        dbContext.Roles.Update(role);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        dbContext.Roles.Remove(new Role { Id = roleId });
        await dbContext.SaveChangesAsync();
    }
}