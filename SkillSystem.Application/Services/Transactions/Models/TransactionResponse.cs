using SkillSystem.Application.Services.Salaries.Models;

namespace SkillSystem.Application.Services.Transactions.Models;

public record TransactionResponse
{
    public int SalaryId { get; init; }
    public SalaryResponse Salary { get; init; }
    public Guid ManagerId { get; init; }
    public DateTime SalaryChangeDate { get; init; }
}
