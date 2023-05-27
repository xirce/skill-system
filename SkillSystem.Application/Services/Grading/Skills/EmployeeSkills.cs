using SkillSystem.Core.Entities;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Grading.Skills;

public static class EmployeeSkills
{
    public static EmployeeSkill Received(Guid employeeId, int skillId)
    {
        return Create(employeeId, skillId, EmployeeSkillStatus.Received);
    }

    public static EmployeeSkill Approved(Guid employeeId, int skillId)
    {
        return Create(employeeId, skillId, EmployeeSkillStatus.Approved);
    }

    private static EmployeeSkill Create(Guid employeeId, int skillId, EmployeeSkillStatus status)
    {
        return new EmployeeSkill
        {
            EmployeeId = employeeId,
            SkillId = skillId,
            Status = status
        };
    }
}
