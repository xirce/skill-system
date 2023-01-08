using SkillSystem.Core.Enums;

namespace SkillSystem.Core.Entities;

/// <summary>
/// Грейд сотрудник.
/// </summary>
public class EmployeeGrade
{
    /// <summary>
    /// Идентификатор сотрудника.
    /// </summary>
    public string EmployeeId { get; set; }

    /// <summary>
    /// Грейд, которого достиг сотрудник.
    /// </summary>
    public Grade Grade { get; set; }

    public int GradeId { get; set; }

    /// <summary>
    /// Статус грейда сотрудника.
    /// </summary>
    public GradeStatus Status { get; set; }
}