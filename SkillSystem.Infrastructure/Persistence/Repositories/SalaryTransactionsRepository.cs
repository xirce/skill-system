using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Repositories.SalaryTransactions;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class SalaryTransactionsRepository : ISalaryTransactionsRepository
{
    private readonly SkillSystemDbContext dbContext;

    public SalaryTransactionsRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateTransactionAsync(SalaryTransaction transaction)
    {
        await dbContext.SalaryTransactions.AddAsync(transaction);
    }

    public async Task<IEnumerable<SalaryTransaction>> GetTransactionsAsync(DateTime? from, DateTime? to)
    {
        return await dbContext.SalaryTransactions
            .Where(transaction => from == null || transaction.SalaryChangeDate >= from ||
            (transaction.SalaryChangeDate.Month == from.Value.Month && transaction.SalaryChangeDate.Year == from.Value.Year))
            .Where(transaction => to == null || transaction.SalaryChangeDate <= to ||
            (transaction.SalaryChangeDate.Month == to.Value.Month && transaction.SalaryChangeDate.Year == to.Value.Year))
            .ToListAsync();
    }

    public async Task<IEnumerable<SalaryTransaction>> GetTransactionsByEmployeeIdAsync(Guid employeeId, DateTime? from, DateTime? to)
    {
        return await dbContext.SalaryTransactions.Where(transaction => transaction.EmployeeId == employeeId)
            .Where(transaction => from == null || transaction.SalaryChangeDate >= from ||
            (transaction.SalaryChangeDate.Month == from.Value.Month && transaction.SalaryChangeDate.Year == from.Value.Year))
            .Where(transaction => to == null || transaction.SalaryChangeDate <= to ||
            (transaction.SalaryChangeDate.Month == to.Value.Month && transaction.SalaryChangeDate.Year == to.Value.Year))
            .ToListAsync();
    }

    public async Task<IEnumerable<SalaryTransaction>> GetTransactionsByManagerIdAsync(Guid managerId, DateTime? from, DateTime? to)
    {
        return await dbContext.SalaryTransactions.Where(transaction => transaction.ChangedBy == managerId)
            .Where(transaction => from == null || transaction.SalaryChangeDate >= from ||
            (transaction.SalaryChangeDate.Month == from.Value.Month && transaction.SalaryChangeDate.Year == from.Value.Year))
            .Where(transaction => to == null || transaction.SalaryChangeDate <= to ||
            (transaction.SalaryChangeDate.Month == to.Value.Month && transaction.SalaryChangeDate.Year == to.Value.Year))
            .ToListAsync();
    }
}
