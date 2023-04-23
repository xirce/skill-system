using SkillSystem.Application.Common.Models.Requests;

namespace SkillSystem.Application.Repositories.Projects;

public record ProjectFilter : PaginationQuery
{
    public string? Name { get; init; }
}
