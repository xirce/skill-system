using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.Duties.Models;

public record DutyRequest
{
    [Required]
    [MaxLength(30)]
    public string Title { get; init; }

    [Required]
    [MaxLength(200)]
    public string Description { get; init; }
}