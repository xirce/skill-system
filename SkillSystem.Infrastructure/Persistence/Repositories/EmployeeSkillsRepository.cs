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

    public async Task AddEmployeeSkillsAsync(IEnumerable<EmployeeSkill> employeeSkills)
    {
        foreach (var employeeSkill in employeeSkills)
        {
            var presentSkill = await FindEmployeeSkillAsync(employeeSkill.EmployeeId, employeeSkill.SkillId);
            if (presentSkill is null)
                await dbContext.EmployeeSkills.AddAsync(employeeSkill);
        }
    }

    public async Task<EmployeeSkill?> FindEmployeeSkillAsync(Guid employeeId, int skillId)
    {
        return await dbContext.EmployeeSkills
            .AsNoTracking()
            .Include(employeeSkill => employeeSkill.Skill)
            .FirstOrDefaultAsync(
                employeeSkill => employeeSkill.EmployeeId == employeeId && employeeSkill.SkillId == skillId);
    }

    public async Task<EmployeeSkill> GetEmployeeSkillAsync(Guid employeeId, int skillId)
    {
        return await FindEmployeeSkillAsync(employeeId, skillId)
               ?? throw new EntityNotFoundException(nameof(EmployeeSkill), new { employeeId, skillId });
    }

    public async Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(Guid employeeId)
    {
        return await QueryEmployeeSkills(employeeId).ToListAsync();
    }

    public async Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(
        Guid employeeId,
        IEnumerable<int> skillsIds)
    {
        return await QueryEmployeeSkills(employeeId)
            .Where(employeeSkill => skillsIds.Contains(employeeSkill.SkillId))
            .ToListAsync();
    }

    public void UpdateSkills(IEnumerable<EmployeeSkill> employeeSkills)
    {
        dbContext.EmployeeSkills.UpdateRange(employeeSkills);
    }

    public void DeleteEmployeeSkills(IEnumerable<EmployeeSkill> skills)
    {
        dbContext.EmployeeSkills.RemoveRange(skills);
    }

    private IQueryable<EmployeeSkill> QueryEmployeeSkills(Guid employeeId)
    {
        return dbContext.EmployeeSkills
            .AsNoTracking()
            .Include(employeeSkill => employeeSkill.Skill)
            .Where(employeeSkill => employeeSkill.EmployeeId == employeeId);
    }
}
