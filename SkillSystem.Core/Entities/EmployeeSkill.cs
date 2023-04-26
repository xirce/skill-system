using SkillSystem.Core.Enums;

namespace SkillSystem.Core.Entities;

/// <summary>
/// Знание/навык сотрудника.
/// </summary>
public class EmployeeSkill
{
    public string EmployeeId { get; set; }

    /// <summary>
    /// Скилл, который получил сотрудник.
    /// </summary>
    public Skill Skill { get; set; }

    public int SkillId { get; set; }

    /// <summary>
    /// Статус скилла сотрудника.
    /// </summary>
    public EmployeeSkillStatus Status { get; set; }
}
