using SkillSystem.Application.Repositories.Duties.Filters;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Duties;

public interface IDutiesRepository
{
    Task CreateDutyAsync(Duty duty);
    Task<Duty?> FindDutyByIdAsync(int dutyId);
    Task<Duty> GetDutyByIdAsync(int dutyId);
    IQueryable<Duty> FindDuties(DutyFilter? filter = default);
    void UpdateDuty(Duty duty);
    void DeleteDuty(Duty duty);
}
