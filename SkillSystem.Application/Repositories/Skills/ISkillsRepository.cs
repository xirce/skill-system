using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Skills;

/// <summary>
/// Общий пул знаний.
/// </summary>
public interface ISkillsRepository : ISkillsReadOnlyRepository
{
    Task CreateSkillAsync(Skill skill);
    void UpdateSkill(Skill skill);
    void DeleteSkill(Skill skill);
}
