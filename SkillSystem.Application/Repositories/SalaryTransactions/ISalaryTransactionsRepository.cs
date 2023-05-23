using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.SalaryTransactions;

public interface ISalaryTransactionsRepository
{
    Task CreateTransactionAsync(SalaryTransaction transaction);
    Task<IEnumerable<SalaryTransaction>> GetTransactionsByEmployeeIdAsync(Guid employeeId, DateTime? from, DateTime? to);
    Task<IEnumerable<SalaryTransaction>> GetTransactionsByManagerIdAsync(Guid managerId, DateTime? from, DateTime? to);
    Task<IEnumerable<SalaryTransaction>> GetTransactionsAsync(DateTime? from, DateTime? to);
}
