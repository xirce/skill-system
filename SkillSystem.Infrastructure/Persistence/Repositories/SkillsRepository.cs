using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class SkillsRepository : ISkillsRepository
{
    private readonly SkillSystemDbContext dbContext;

    public SkillsRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateSkillAsync(Skill skill)
    {
        await dbContext.Skills.AddAsync(skill);
        await dbContext.SaveChangesAsync();
        return skill.Id;
    }

    public async Task<Skill?> FindSkillByIdAsync(int skillId, bool includeSubSkills = false)
    {
        var skills = dbContext.Skills.AsNoTracking();

        if (includeSubSkills)
            skills = skills.Include(skill => skill.SubSkills.OrderBy(subSkill => subSkill.Id));

        return await skills.FirstOrDefaultAsync(skill => skill.Id == skillId);
    }

    public IQueryable<Skill> FindSkills(string? title = default)
    {
        var query = dbContext.Skills.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(skill => skill.Title.Contains(title));

        return query.OrderBy(skill => skill.Id);
    }

    public async Task UpdateSkillAsync(Skill skill)
    {
        dbContext.Skills.Update(skill);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteSkillAsync(int skillId)
    {
        dbContext.Skills.Remove(new Skill { Id = skillId });
        await dbContext.SaveChangesAsync();
    }
}