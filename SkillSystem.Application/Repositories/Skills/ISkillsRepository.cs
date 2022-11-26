using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Skills;

/// <summary>
/// Общий пул знаний.
/// </summary>
public interface ISkillsRepository
{
    Task<int> CreateSkillAsync(Skill skill);
    IQueryable<Skill> FindSkills(string? title = default);
    Task<Skill?> FindSkillByIdAsync(int skillId, bool includeSubSkills = false);
    Task UpdateSkillAsync(Skill skill);
    Task DeleteSkillAsync(int skillId);
}