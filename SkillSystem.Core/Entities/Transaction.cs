using System.ComponentModel.DataAnnotations;
namespace SkillSystem.Core.Entities;

/// <summary>
/// Зарплата сотрудника.
/// </summary>
public class Transaction
{
    [Key]
    public int SalaryId { get; set; }

    public Salary Salary { get; set; }

    /// <summary>
    /// Id руководителя, назначившего изменение зарплаты.
    /// </summary>
    public Guid ManagerId { get; set; }

    /// <summary>
    /// Дата изменения зарплаты.
    /// </summary>
    public DateTime SalaryChangeDate { get; set; }
}
