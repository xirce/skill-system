using Mapster;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.SalaryTransactions;
using SkillSystem.Application.Services.SalaryTransactions.Models;

namespace SkillSystem.Application.Services.SalaryTransactions;

public class SalaryTransactionsService : ISalaryTransactionsService
{
    private readonly ISalaryTransactionsRepository transactionsRepository;

    public SalaryTransactionsService(ISalaryTransactionsRepository transactionsRepository,
        IUnitOfWork unitOfWork)
    {  
        this.transactionsRepository = transactionsRepository;
    }

    public async Task<ICollection<SalaryTransactionResponse>> GetTransactionsAsync(DateTime? from, DateTime? to)
    {
        var transactions = await transactionsRepository.GetTransactionsAsync(from, to);
        var sortedTransactions = transactions.OrderBy(transactions => transactions.SalaryChangeDate);
        return sortedTransactions.Adapt<ICollection<SalaryTransactionResponse>>();
    }

    public async Task<ICollection<SalaryTransactionResponse>> GetTransactionsByEmployeeIdAsync(Guid employeeId, DateTime? from, DateTime? to)
    {
        var transactions = await transactionsRepository.GetTransactionsByEmployeeIdAsync(employeeId, from, to);
        var sortedTransactions = transactions.OrderBy(transactions => transactions.SalaryChangeDate);
        return sortedTransactions.Adapt<ICollection<SalaryTransactionResponse>>();
    }

    public async Task<ICollection<SalaryTransactionResponse>> GetTransactionsByManagerIdAsync(Guid managerId, DateTime? from, DateTime? to)
    {
        var transactions = await transactionsRepository.GetTransactionsByManagerIdAsync(managerId, from, to);
        var sortedTransactions = transactions.OrderBy(transactions => transactions.SalaryChangeDate);
        return sortedTransactions.Adapt<ICollection<SalaryTransactionResponse>>();
    }
}
