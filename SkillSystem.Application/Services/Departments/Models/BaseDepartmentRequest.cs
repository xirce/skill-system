using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.Departments.Models;

public record BaseDepartmentRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
}
