namespace SkillSystem.Core.Entities;

/// <summary>
/// Промежуточная сущность для связи должности и обязанности.
/// </summary>
public class PositionDuty
{
    /// <summary>
    /// Должность.
    /// </summary>
    public Position Position { get; set; }
    public int PositionId { get; set; }

    /// <summary>
    /// Обязанность.
    /// </summary>
    public Duty Duty { get; set; }
    public int DutyId { get; set; }
}