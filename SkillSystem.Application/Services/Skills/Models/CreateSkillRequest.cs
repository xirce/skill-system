namespace SkillSystem.Application.Services.Skills.Models;

public record CreateSkillRequest : BaseSkillRequest
{
    public int? GroupId { get; init; }
};
