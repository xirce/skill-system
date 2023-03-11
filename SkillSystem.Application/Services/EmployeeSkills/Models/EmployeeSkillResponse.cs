using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.Application.Services.EmployeeSkills.Models;

public record EmployeeSkillResponse
{
    public SkillShortInfo Skill { get; init; }
    public ICollection<EmployeeSkillShortInfo> SubSkills { get; init; }
    public bool IsApproved { get; init; }
}
