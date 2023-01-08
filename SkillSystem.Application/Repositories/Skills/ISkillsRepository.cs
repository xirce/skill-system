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
    Task<Skill> GetSkillByIdAsync(int skillId, bool includeSubSkills = false);
    Task<IEnumerable<Skill>> GetSubSkillsAsync(int groupId);
    Task<IEnumerable<Skill>> TraverseSkillAsync(int groupId);
    IAsyncEnumerable<Skill> GetGroups(int skillId);
    Task UpdateSkillAsync(Skill skill);
    Task DeleteSkillAsync(int skillId);
}