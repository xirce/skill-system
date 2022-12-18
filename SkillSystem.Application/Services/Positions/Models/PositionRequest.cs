using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.Positions.Models;

public record PositionRequest
{
    [Required]
    [MaxLength(30)]
    public string Title { get; init; }
}