using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Duties;
using SkillSystem.Application.Repositories.Duties.Filters;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class DutiesRepository : IDutiesRepository
{
    private readonly SkillSystemDbContext dbContext;

    public DutiesRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateDutyAsync(Duty duty)
    {
        await dbContext.Duties.AddAsync(duty);
    }

    public async Task<Duty?> FindDutyByIdAsync(int dutyId)
    {
        return await dbContext.Duties.FirstOrDefaultAsync(duty => duty.Id == dutyId);
    }

    public async Task<Duty> GetDutyByIdAsync(int dutyId)
    {
        return await FindDutyByIdAsync(dutyId) ?? throw new EntityNotFoundException(nameof(Duty), dutyId);
    }

    public IQueryable<Duty> FindDuties(DutyFilter? filter = default)
    {
        var duties = dbContext.Duties.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Title))
            duties = duties.Where(role => role.Title.Contains(filter.Title));

        return duties.OrderBy(duty => duty.Id);
    }

    public void UpdateDuty(Duty duty)
    {
        dbContext.Duties.Update(duty);
    }

    public void DeleteDuty(Duty duty)
    {
        dbContext.Duties.Remove(duty);
    }
}
