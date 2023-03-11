namespace SkillSystem.Application.Services.EmployeeSkills.Models;

public record EmployeeSkillStatus
{
    public int SkillId { get; init; }
    public bool IsApproved { get; init; }
}
