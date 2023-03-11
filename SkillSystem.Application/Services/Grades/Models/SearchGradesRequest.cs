using SkillSystem.Application.Common.Models.Requests;

namespace SkillSystem.Application.Services.Grades.Models;

public record SearchGradesRequest : PaginationQuery
{
    public string? Title { get; set; }
}
