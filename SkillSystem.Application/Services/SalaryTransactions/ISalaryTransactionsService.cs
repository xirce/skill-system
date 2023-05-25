using SkillSystem.Application.Services.SalaryTransactions.Models;

namespace SkillSystem.Application.Services.SalaryTransactions;

public interface ISalaryTransactionsService
{
    Task<ICollection<SalaryTransactionResponse>> GetTransactionsByEmployeeIdAsync(Guid employeeId, DateTime? from, DateTime? to);
    Task<ICollection<SalaryTransactionResponse>> GetTransactionsByManagerIdAsync(Guid managerId, DateTime? from, DateTime? to);
    Task<ICollection<SalaryTransactionResponse>> GetTransactionsAsync(DateTime? from, DateTime? to);
}
