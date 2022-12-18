using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.Grades.Models;

public record GradeRequest
{
    [Required]
    [MaxLength(30)]
    public string Title { get; init; }
}