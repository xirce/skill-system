namespace SkillSystem.Core.Entities;


public class SalaryTransaction : BaseEntity
{
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
    public Guid ManagerId { get; set; }
    public DateTime SalaryChangeDate { get; set; }
}
