namespace SkillSystem.Application.Services.Skills.Models;

public record SkillResponse : SkillShortInfo
{
    public IEnumerable<SkillShortInfo> SubSkills { get; init; }
}