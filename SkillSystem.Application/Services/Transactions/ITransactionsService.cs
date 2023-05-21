using SkillSystem.Application.Services.Transactions.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Transactions;

public interface ITransactionsService
{
    Task<int> SaveTransactionAsync(Salary salary, Guid managerId);
    Task<ICollection<TransactionResponse>> GetTransactionsByEmployeeIdAsync(Guid employeeId, DateTime? from, DateTime? to);
    Task<ICollection<TransactionResponse>> GetTransactionsByManagerIdAsync(Guid managerId, DateTime? from, DateTime? to);
    Task<ICollection<TransactionResponse>> GetTransactionsAsync(DateTime? from, DateTime? to);
}
