using SkillSystem.Application.Services.Salaries.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Salaries;

public interface ISalariesService
{
    Task<Salary> SaveSalaryAsync(SalaryRequest request);
    Task<SalaryResponse> GetSalaryByIdAsync(int salaryId);
    Task<SalaryResponse> GetSalaryByMonthAsync(Guid employeeId, DateTime month);
    Task<SalaryResponse> GetCurrentSalaryAsync(Guid employeeId);
    Task<ICollection<SalaryResponse>> GetSalariesAsync(Guid employeeId, DateTime? from, DateTime? to);
    Task CancelSalaryAssigmentAsync(int salaryId);
}
