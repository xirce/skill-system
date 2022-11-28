using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Core.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}