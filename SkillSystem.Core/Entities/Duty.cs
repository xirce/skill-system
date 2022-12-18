namespace SkillSystem.Core.Entities;

/// <summary>
/// Представляет то, что сотрудник должен выполнять в проекте.
/// </summary>
public class Duty : BaseEntity
{
    /// <summary>
    /// Название обязанности.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание обязанности.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Набор должностей, в которых есть данная обязанность.
    /// </summary>
    public ICollection<Position> Positions { get; set; }

    public ICollection<PositionDuty> PositionDuties { get; set; }
}