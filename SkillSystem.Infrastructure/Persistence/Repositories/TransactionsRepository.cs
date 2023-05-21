using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Transactions;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class TransactionsRepository : ITransactionsRepository
{
    private readonly SkillSystemDbContext dbContext;

    public TransactionsRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateTransactionAsync(Transaction transaction)
    {
        await dbContext.AddAsync(transaction);
        await dbContext.SaveChangesAsync();
        return transaction.SalaryId;
    }

    private async Task<Transaction?> FindTransactionByIdAsync(int salaryId)
    {
        return await dbContext.Transactions.FirstOrDefaultAsync(transaction => transaction.SalaryId == salaryId);
    }

    public async Task<Transaction> GetTransactionByIdAsync(int salaryId)
    {
        return await FindTransactionByIdAsync(salaryId) ?? throw new EntityNotFoundException(nameof(Transaction), salaryId);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(DateTime? from, DateTime? to)
    {
        var transactions = dbContext.Transactions
            .Where(transaction => from == null || transaction.SalaryChangeDate >= from ||
            (transaction.SalaryChangeDate.Month == from.Value.Month && transaction.SalaryChangeDate.Year == from.Value.Year))
            .Where(transaction => to == null || transaction.SalaryChangeDate <= to ||
            (transaction.SalaryChangeDate.Month == to.Value.Month && transaction.SalaryChangeDate.Year == to.Value.Year));

        return await transactions.ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByEmployeeIdAsync(Guid employeeId, DateTime? from, DateTime? to)
    {
        var transactions = dbContext.Transactions.Where(transaction => transaction.Salary.EmployeeId == employeeId)
            .Where(transaction => from == null || transaction.SalaryChangeDate >= from ||
            (transaction.SalaryChangeDate.Month == from.Value.Month && transaction.SalaryChangeDate.Year == from.Value.Year))
            .Where(transaction => to == null || transaction.SalaryChangeDate <= to ||
            (transaction.SalaryChangeDate.Month == to.Value.Month && transaction.SalaryChangeDate.Year == to.Value.Year));

        return await transactions.ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByManagerIdAsync(Guid managerId, DateTime? from, DateTime? to)
    {
        var transactions = dbContext.Transactions.Where(transaction => transaction.ManagerId == managerId)
            .Where(transaction => from == null || transaction.SalaryChangeDate >= from ||
            (transaction.SalaryChangeDate.Month == from.Value.Month && transaction.SalaryChangeDate.Year == from.Value.Year))
            .Where(transaction => to == null || transaction.SalaryChangeDate <= to ||
            (transaction.SalaryChangeDate.Month == to.Value.Month && transaction.SalaryChangeDate.Year == to.Value.Year));

        return await transactions.ToListAsync();
    }

    public async Task<int> UpdateTransactionAsync(Transaction transaction)
    {
        dbContext.Transactions.Update(transaction);
        await dbContext.SaveChangesAsync();
        return transaction.SalaryId;
    }
}
