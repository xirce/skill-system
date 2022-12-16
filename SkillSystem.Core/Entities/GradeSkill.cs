namespace SkillSystem.Core.Entities;

/// <summary>
/// Промежуточная сущность для связи грейда и скилла.
/// </summary>
public class GradeSkill
{
    /// <summary>
    /// Грейд.
    /// </summary>
    public Grade Grade { get; set; }

    public int GradeId { get; set; }

    /// <summary>
    /// Скилл.
    /// </summary>
    public Skill Skill { get; set; }

    public int SkillId { get; set; }
}