using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Skills.Models;

public record SkillShortInfo
{
    public int Id { get; init; }
    public string Title { get; init; }
    public SkillType Type { get; init; }
}
