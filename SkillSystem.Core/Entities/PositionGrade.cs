namespace SkillSystem.Core.Entities;

/// <summary>
/// Промежуточная сущность для связи должности и грейда.
/// </summary>
public class PositionGrade
{
    /// <summary>
    /// Должность.
    /// </summary>
    public Position Position { get; set; }

    public int PositionId { get; set; }

    /// <summary>
    /// Грейд.
    /// </summary>
    public Grade Grade { get; set; }

    public int GradeId { get; set; }
}
