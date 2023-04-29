using SkillSystem.Core.Entities;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.EmployeeSkills;

public static class EmployeeSkills
{
    public static EmployeeSkill Received(string employeeId, int skillId)
    {
        return Create(employeeId, skillId, EmployeeSkillStatus.Received);
    }

    public static EmployeeSkill Approved(string employeeId, int skillId)
    {
        return Create(employeeId, skillId, EmployeeSkillStatus.Approved);
    }

    private static EmployeeSkill Create(string employeeId, int skillId, EmployeeSkillStatus status)
    {
        return new EmployeeSkill
        {
            EmployeeId = employeeId,
            SkillId = skillId,
            Status = status
        };
    }
}
