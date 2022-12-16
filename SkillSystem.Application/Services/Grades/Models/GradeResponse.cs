using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.Application.Services.Grades.Models;

public record GradeResponse : GradeShortInfo
{
    public int? PrevGradeId { get; init; }
    public int? NextGradeId { get; init; }
    public int RoleId { get; init; }
    public IEnumerable<SkillResponse> Skills { get; init; }
}