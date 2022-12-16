namespace SkillSystem.Core.Entities;

/// <summary>
/// Представляет то, какой частью проекта занимается сотрудник.
/// </summary>
public class Role : BaseEntity
{
    /// <summary>
    /// Название роли.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Грейды, которые есть в этой роли.
    /// </summary>
    public ICollection<Grade> Grades { get; set; }
}