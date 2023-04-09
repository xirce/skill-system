using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Salaries;

public interface ISalariesRepository
{
        Task<int> CreateSalaryAsync(Salary salary);
        Task<Salary?> FindSalaryByIdAsync(int salaryId);
        Task<Salary> GetSalaryByIdAsync(int salaryId);
        Task<Salary?> FindSalaryByMonthAsync(Guid employeeId, DateTime date);
        Task<Salary> GetSalaryByMonthAsync(Guid employeeId, DateTime date);
        Task<IEnumerable<Salary>> GetSalariesAsync(Guid employeeId, DateTime? from, DateTime? to);
        Task<int> UpdateSalaryAsync(Salary salary);
        Task DeleteSalaryAsync(Salary salary);
}
