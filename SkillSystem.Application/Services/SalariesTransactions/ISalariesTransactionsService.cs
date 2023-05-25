using SkillSystem.Application.Services.Salaries.Models;

namespace SkillSystem.Application.Services.SalariesTransactions;

public interface ISalariesTransactionsService
{
    public Task<int> SaveSalary(SalaryRequest request);
}
