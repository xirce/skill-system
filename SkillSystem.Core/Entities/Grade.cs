namespace SkillSystem.Core.Entities;

/// <summary>
/// Фиксированный набор скиллов, определяющий уровень знаний определенной роли.
/// </summary>
public class Grade : BaseEntity
{
    /// <summary>
    /// Название грейда.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Роль, которой принадлежит этот грейд.
    /// </summary>
    public Role Role { get; set; }

    public int RoleId { get; set; }

    /// <summary>
    /// Предыдущий грейд.
    /// </summary>
    public Grade? PrevGrade { get; set; }

    public int? PrevGradeId { get; set; }

    /// <summary>
    /// Следующий грейд.
    /// </summary>
    public Grade? NextGrade { get; set; }

    public int? NextGradeId => NextGrade?.Id;

    /// <summary>
    /// Набор знаний, которые необходимо получить для достижения этого грейда.
    /// </summary>
    public ICollection<Skill> Skills { get; set; }

    public ICollection<GradeSkill> GradeSkills { get; set; }
}