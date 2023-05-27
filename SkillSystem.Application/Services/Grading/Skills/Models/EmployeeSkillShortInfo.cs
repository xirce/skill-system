using SkillSystem.Application.Services.Skills.Models;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Grading.Skills.Models;

public record EmployeeSkillShortInfo
{
    public SkillShortInfo Skill { get; init; }
    public EmployeeSkillStatus Status { get; init; }
}
