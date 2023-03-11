using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.Roles.Models;

public record RoleRequest
{
    [Required]
    [MaxLength(30)]
    public string Title { get; set; }
}
