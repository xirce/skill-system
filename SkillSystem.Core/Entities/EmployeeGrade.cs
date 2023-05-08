using SkillSystem.Core.Enums;

namespace SkillSystem.Core.Entities;

/// <summary>
/// Грейд сотрудник.
/// </summary>
public class EmployeeGrade
{
    /// <summary>
    /// Сотрудник.
    /// </summary>
    public Employee Employee { get; set; }

    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Грейд.
    /// </summary>
    public Grade Grade { get; set; }

    public int GradeId { get; set; }

    /// <summary>
    /// Статус грейда сотрудника.
    /// </summary>
    public EmployeeGradeStatus Status { get; set; }
}
