namespace SkillSystem.Application.Services.SalaryTransactions.Models;

public record SalaryTransactionResponse
{
    public int Id { get; init; }
    public Guid EmployeeId { get; init; }
    public decimal Wage { get; init; }
    public decimal Rate { get; init; }
    public decimal Bonus { get; init; }
    public DateTime StartDate { get; init; }
    public Guid ManagerId { get; init; }
    public DateTime SalaryChangeDate { get; init; }
}
