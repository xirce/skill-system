namespace SkillSystem.Core.Entities;

/// <summary>
/// Проект, представляет собой продукт и набор ролей. 
/// </summary>
public class Project : BaseEntity
{
    /// <summary>
    /// Название проекта.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Набор ролей проекта.
    /// </summary>
    public ICollection<ProjectRole> Roles { get; set; }
}