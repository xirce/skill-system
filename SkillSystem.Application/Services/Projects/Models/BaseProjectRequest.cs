using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.Projects.Models;

public record BaseProjectRequest
{
    [Required]
    public string Name { get; init; }
}
