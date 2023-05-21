using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Transactions;

public interface ITransactionsRepository
{
    Task<int> CreateTransactionAsync(Transaction transaction);
    Task<int> UpdateTransactionAsync(Transaction transaction);
    Task<Transaction> GetTransactionByIdAsync(int salaryId);
    Task<IEnumerable<Transaction>> GetTransactionsByEmployeeIdAsync(Guid employeeId, DateTime? from, DateTime? to);
    Task<IEnumerable<Transaction>> GetTransactionsByManagerIdAsync(Guid managerId, DateTime? from, DateTime? to);
    Task<IEnumerable<Transaction>> GetTransactionsAsync(DateTime? from, DateTime? to);
}
