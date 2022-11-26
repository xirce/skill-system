using System.ComponentModel.DataAnnotations;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Skills.Models;

public record BaseSkillRequest
{
    [Required]
    [MaxLength(30)]
    public string Title { get; set; }

    public SkillType Type { get; set; }
}