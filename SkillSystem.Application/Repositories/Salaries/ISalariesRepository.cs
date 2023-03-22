using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Salaries;

public interface ISalariesRepository
{
        Task<int> CreateSalaryAsync(Salary salary);
        Task<Salary?> FindSalaryByIdAsync(int salaryId);
        Task<Salary> GetSalaryByIdAsync(int salaryId);
        Task<IEnumerable<Salary>> GetSalariesAsync(Guid? employeeId, DateTime? from, DateTime? to);
        Task UpdateSalaryAsync(Salary salary);
        Task DeleteSalaryAsync(Salary salary);
}
