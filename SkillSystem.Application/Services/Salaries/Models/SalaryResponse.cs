namespace SkillSystem.Application.Services.Salaries.Models;

public record SalaryResponse
{
    public int Id { get; init; }
    public Guid EmployeeId { get; init; }
    public decimal Wage { get; init; }
    public decimal Rate { get; init; }
    public decimal Bonus { get; init; }
    public DateTime StartDate { get; init; }
}
