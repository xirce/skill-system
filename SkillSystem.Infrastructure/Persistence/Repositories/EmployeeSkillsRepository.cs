using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class EmployeeSkillsRepository : IEmployeeSkillsRepository
{
    private readonly SkillSystemDbContext dbContext;

    public EmployeeSkillsRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddEmployeeSkillsAsync(params EmployeeSkill[] employeeSkills)
    {
        foreach (var employeeSkill in employeeSkills)
        {
            var presentSkill = await FindEmployeeSkillAsync(employeeSkill.EmployeeId, employeeSkill.SkillId);
            if (presentSkill is null)
                await dbContext.EmployeeSkills.AddAsync(employeeSkill);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<EmployeeSkill?> FindEmployeeSkillAsync(string employeeId, int skillId)
    {
        return await dbContext.EmployeeSkills
            .Include(employeeSkill => employeeSkill.Skill)
            .FirstOrDefaultAsync(
                employeeSkill => employeeSkill.EmployeeId == employeeId && employeeSkill.SkillId == skillId
            );
    }

    public async Task<EmployeeSkill> GetEmployeeSkillAsync(string employeeId, int skillId)
    {
        return await FindEmployeeSkillAsync(employeeId, skillId)
               ?? throw new EntityNotFoundException(nameof(EmployeeSkill), new { employeeId, skillId });
    }

    public async Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(string employeeId)
    {
        return await QueryEmployeeSkills(employeeId).ToListAsync();
    }

    public async Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(
        string employeeId,
        IEnumerable<int> skillsIds
    )
    {
        return await QueryEmployeeSkills(employeeId)
            .Where(employeeSkill => skillsIds.Contains(employeeSkill.SkillId))
            .ToListAsync();
    }

    public async Task UpdateSkillsAsync(params EmployeeSkill[] employeeSkills)
    {
        dbContext.EmployeeSkills.UpdateRange(employeeSkills);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteEmployeeSkillsAsync(params EmployeeSkill[] skills)
    {
        dbContext.EmployeeSkills.RemoveRange(skills);
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<EmployeeSkill> QueryEmployeeSkills(string employeeId)
    {
        return dbContext.EmployeeSkills
            .AsNoTracking()
            .Include(employeeSkill => employeeSkill.Skill)
            .Where(employeeSkill => employeeSkill.EmployeeId == employeeId);
    }
}