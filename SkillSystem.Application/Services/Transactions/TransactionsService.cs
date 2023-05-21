using Mapster;
using SkillSystem.Application.Repositories.Salaries;
using SkillSystem.Application.Repositories.Transactions;
using SkillSystem.Application.Services.Salaries.Models;
using SkillSystem.Application.Services.Transactions.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Transactions;

public class TransactionsService : ITransactionsService
{
    private readonly ITransactionsRepository transactionsRepository;
    private readonly ISalariesRepository salariesRepository;

    public TransactionsService(ITransactionsRepository transactionsRepository, ISalariesRepository salariesRepository)
    {
        this.transactionsRepository = transactionsRepository;
        this.salariesRepository = salariesRepository;
    }

    public async Task<int> SaveTransactionAsync(Salary salary, Guid managerId)
    {
        var transactionById = await transactionsRepository.GetTransactionByIdAsync(salary.Id);
        var transaction = new Transaction { SalaryId = salary.Id, ManagerId = managerId, SalaryChangeDate = DateTime.UtcNow, Salary = salary };
        if (transactionById != null)
            return await transactionsRepository.UpdateTransactionAsync(transaction);
        return await transactionsRepository.CreateTransactionAsync(transaction);
    }

    public async Task<ICollection<TransactionResponse>> GetTransactionsAsync(DateTime? from, DateTime? to)
    {
        var transactions = await transactionsRepository.GetTransactionsAsync(from, to);
        var sortedTransactions = transactions.Select(transaction => GetTransactionWithSalry(transaction).Result)
                        .OrderBy(transactions => transactions.SalaryChangeDate);
        return sortedTransactions.Adapt<ICollection<TransactionResponse>>();
    }

    public async Task<ICollection<TransactionResponse>> GetTransactionsByEmployeeIdAsync(Guid employeeId, DateTime? from, DateTime? to)
    {
        var transactions = await transactionsRepository.GetTransactionsByEmployeeIdAsync(employeeId, from, to);
        var sortedTransactions = transactions.Select(transaction => GetTransactionWithSalry(transaction).Result)
                        .OrderBy(transactions => transactions.SalaryChangeDate);
        return sortedTransactions.Adapt<ICollection<TransactionResponse>>();
    }

    public async Task<ICollection<TransactionResponse>> GetTransactionsByManagerIdAsync(Guid managerId, DateTime? from, DateTime? to)
    {
        var transactions = await transactionsRepository.GetTransactionsByManagerIdAsync(managerId, from, to);
        var sortedTransactions = transactions.Select(transaction => GetTransactionWithSalry(transaction).Result)
                        .OrderBy(transactions => transactions.SalaryChangeDate);
        return sortedTransactions.Adapt<ICollection<TransactionResponse>>();
    }

    private async Task<Transaction> GetTransactionWithSalry(Transaction transaction)
    {
        var salary = await salariesRepository.GetSalaryByIdAsync(transaction.SalaryId);
        transaction.Salary = salary;
        return transaction;
    }
}
