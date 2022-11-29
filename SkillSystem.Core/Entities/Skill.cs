using SkillSystem.Core.Enums;

namespace SkillSystem.Core.Entities;

/// <summary>
/// Знание/понимание чего-либо.
/// </summary>
public class Skill : BaseEntity
{
    /// <summary>
    /// Название знания.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Тип знания.
    /// </summary>
    public SkillType Type { get; set; }

    /// <summary>
    /// Группа знаний (тема), которой принадлежит это знание.
    /// </summary>
    public Skill? Group { get; set; }

    public int? GroupId { get; set; }

    /// <summary>
    /// Набор знаний, объединенных темой этого знания.
    /// </summary>
    public ICollection<Skill> SubSkills { get; set; }

    /// <summary>
    /// Набор грейдов, в которых есть этот скилл.
    /// </summary>
    public ICollection<Grade> Grades { get; set; }

    public ICollection<GradeSkill> GradeSkills { get; set; }
}