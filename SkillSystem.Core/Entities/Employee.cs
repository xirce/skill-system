using SkillSystem.Core.Enums;

namespace SkillSystem.Core.Entities;

/// <summary>
/// Сотрудник.
/// </summary>
public class Employee
{
    public Guid Id { get; set; }

    /// <summary>
    /// Тип сотрудника (руководитель, обычный сотрудник)
    /// </summary>
    public EmployeeType Type { get; set; }

    /// <summary>
    /// Руководитель сотрудника, если он есть.
    /// </summary>
    public Employee? Manager { get; set; }

    public Guid? ManagerId { get; set; }
}
