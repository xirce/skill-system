using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.Application.Services.EmployeeSkills.Models;

public record EmployeeSkillShortInfo
{
    public SkillShortInfo Skill { get; init; }
    public bool IsApproved { get; init; }
}
