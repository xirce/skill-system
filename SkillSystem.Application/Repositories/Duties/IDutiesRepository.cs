using SkillSystem.Application.Repositories.Duties.Filters;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Duties;

public interface IDutiesRepository
{
    Task<int> CreateDutyAsync(Duty duty);
    Task<Duty?> FindDutyByIdAsync(int dutyId);
    Task<Duty> GetDutyByIdAsync(int dutyId);
    IQueryable<Duty> FindDuties(DutyFilter? filter = default);
    Task UpdateDutyAsync(Duty duty);
    Task DeleteDutyAsync(Duty duty);
}
