using System.Diagnostics.CodeAnalysis;

namespace SkillSystem.Core.Entities;

/// <summary>
/// Роль в проекте, может быть занята сотрудником.
/// </summary>
public class ProjectRole : BaseEntity
{
    /// <summary>
    /// Проект.
    /// </summary>
    public Project Project { get; set; }

    public int ProjectId { get; set; }

    /// <summary>
    /// Роль.
    /// </summary>
    public Role Role { get; set; }

    public int RoleId { get; set; }

    /// <summary>
    /// Идентификатор сотрудника, который занимает данную роль.
    /// <returns>null, если роль в проекте свободна.</returns>
    /// </summary>
    public string? EmployeeId { get; set; }

    /// <summary>
    /// Значение, указывающее, свободна ли роль на проекте.
    /// </summary>
    [MemberNotNullWhen(false, nameof(EmployeeId))]
    public bool IsFree => EmployeeId == null;
}