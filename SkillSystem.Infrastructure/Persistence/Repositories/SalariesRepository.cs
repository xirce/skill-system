using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Salaries;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class SalariesRepository : ISalariesRepository
{
    private readonly SkillSystemDbContext dbContext;

    public SalariesRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateSalaryAsync(Salary salary)
    {
        await dbContext.AddAsync(salary);
        await dbContext.SaveChangesAsync();
        return salary.Id;
    }

    public async Task<Salary?> FindSalaryByIdAsync(int salaryId)
    {
        return await dbContext.Salaries.OrderBy(salary => salary.SalaryDate).LastOrDefaultAsync(salary => salary.Id == salaryId);
    }

    public async Task<Salary> GetSalaryByIdAsync(int salaryId)
    {
        return await FindSalaryByIdAsync(salaryId) ?? throw new EntityNotFoundException(nameof(Salary), salaryId);
    }

    public async Task<IEnumerable<Salary>> GetSalariesAsync(Guid? employeeId, DateTime? from, DateTime? to)
    {
        var salaries = dbContext.Salaries.ToAsyncEnumerable();
        if (employeeId != null)
            salaries = salaries.Where(salary => salary.EmployeeId == employeeId);
        if (from != null)
            salaries = salaries.Where(salary => salary.SalaryDate >= from);
        if (to != null)
            salaries = salaries.Where(salary => salary.SalaryDate <= to);

        return await salaries.ToListAsync();
    }

    public async Task UpdateSalaryAsync(Salary salary)
    {
        dbContext.Salaries.Update(salary);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteSalaryAsync(Salary salary)
    {
        dbContext.Salaries.Remove(salary);
        await dbContext.SaveChangesAsync();
    }
}
