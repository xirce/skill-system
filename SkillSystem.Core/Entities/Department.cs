namespace SkillSystem.Core.Entities;

/// <summary>
/// Отдел.
/// </summary>
public class Department : BaseEntity
{
    /// <summary>
    /// Название отдела.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание отдела.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Руководитель отдела.
    /// </summary>
    public Employee? HeadEmployee { get; set; }

    public Guid? HeadEmployeeId { get; set; }

    /// <summary>
    /// Сотрудники, которые находятся в отделе.
    /// </summary>
    public ICollection<Employee> Employees { get; set; }
}
