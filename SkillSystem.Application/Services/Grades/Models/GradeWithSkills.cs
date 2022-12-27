using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.Application.Services.Grades.Models;

public record GradeWithSkills : GradeShortInfo
{
    public ICollection<SkillShortInfo> Skills { get; init; }
}