namespace SkillSystem.Application.Services.Grading.Skills;

public interface IEmployeeSkillsChangeStrategy
{
    Task<EmployeeSkillsChangeResult> AddSkill(Guid employeeId, int skillId);
    Task<EmployeeSkillsChangeResult> ApproveSkill(Guid employeeId, int skillId);
    Task<EmployeeSkillsChangeResult> DeleteSkill(Guid employeeId, int skillId);
}
