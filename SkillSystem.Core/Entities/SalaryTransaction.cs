namespace SkillSystem.Core.Entities;


/// <summary>
/// Зарплатная транзакция.
/// </summary>
public class SalaryTransaction : BaseEntity
{
    /// <summary>
    /// Id сотрудника.
    /// </summary>
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Измененный оклад.
    /// </summary>
    public decimal Wage { get; set; }

    /// <summary>
    /// Измененная ставка.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Измененная премия.
    /// </summary>
    public decimal Bonus { get; set; }

    /// <summary>
    /// Месяц назначения зарплаты.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Id сотрудника изменившего зарплату.
    /// </summary>
    public Guid ChangedBy { get; set; }

    /// <summary>
    /// Дата изменения зарплаты.
    /// </summary>
    public DateTime SalaryChangeDate { get; set; }
}
