using SkillSystem.Application.Common.Models.Requests;

namespace SkillSystem.Application.Services.Skills.Models;

public record SearchSkillsRequest : PaginationQuery
{
    public string? Title { get; init; }
}