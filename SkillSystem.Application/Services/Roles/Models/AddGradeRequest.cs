using SkillSystem.Application.Services.Grades.Models;

namespace SkillSystem.Application.Services.Roles.Models;

public record AddGradeRequest : GradeRequest
{
    public int? PrevGradeId { get; init; }
}
