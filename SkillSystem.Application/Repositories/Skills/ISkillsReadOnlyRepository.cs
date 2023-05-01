using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Skills;

public interface ISkillsReadOnlyRepository
{
    IQueryable<Skill> FindSkills(string? title = default);
    Task<Skill?> FindSkillByIdAsync(int skillId, bool includeSubSkills = false);
    Task<Skill> GetSkillByIdAsync(int skillId, bool includeSubSkills = false);
    Task<IEnumerable<Skill>> GetSubSkillsAsync(int groupId);
    Task<IEnumerable<Skill>> TraverseSkillAsync(int groupId);
    IAsyncEnumerable<Skill> GetGroups(int skillId);
}
