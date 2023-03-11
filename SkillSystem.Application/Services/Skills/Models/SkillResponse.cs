namespace SkillSystem.Application.Services.Skills.Models;

public record SkillResponse : SkillShortInfo
{
    public int? GroupId { get; init; }
    public IEnumerable<SkillShortInfo> SubSkills { get; init; }
}
