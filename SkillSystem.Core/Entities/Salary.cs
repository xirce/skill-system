namespace SkillSystem.Core.Entities;

/// <summary>
/// Зарплата сотрудника.
/// </summary>
public class Salary : BaseEntity
{
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Оклад.
    /// </summary>
    public decimal Wage { get; set; }

    /// <summary>
    /// Ставка.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Премия.
    /// </summary>
    public decimal Bonus { get; set; }

    /// <summary>
    /// Месяц назначения зарплаты.
    /// </summary>
    public DateTime StartDate { get; set; }

}
