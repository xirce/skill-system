using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.Salaries.Models;

public record SalaryRequest
{
    [Required]
    public Guid EmployeeId { get; init; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Wage { get; init; }

    [Required]
    [Range(0, 1)]
    public decimal Rate { get; init; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Bonus { get; init; }

    [Required]
    public DateTime StartDate { get; init; }
}
