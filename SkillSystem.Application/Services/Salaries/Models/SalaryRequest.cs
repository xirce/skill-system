using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.Salaries.Models;

public record SalaryRequest
{
    [Required]
    public Guid EmployeeId { get; init; }

    [Required]
    public decimal Wage { get; init; }

    [Required]
    public decimal Rate { get; init; }

    [Required]
    public decimal Bonus { get; init; }
}
