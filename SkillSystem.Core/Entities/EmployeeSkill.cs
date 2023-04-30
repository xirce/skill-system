using SkillSystem.Core.Enums;

namespace SkillSystem.Core.Entities;

/// <summary>
/// Знание/навык сотрудника.
/// </summary>
public class EmployeeSkill
{
    /// <summary>
    /// Сотрудник.
    /// </summary>
    public Employee Employee { get; set; }

    public Guid EmployeeId { get; set; }

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
