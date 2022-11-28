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
}