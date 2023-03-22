using SkillSystem.Application.Services.Salaries.Models;

namespace SkillSystem.Application.Services.Salaries;

public interface ISalariesService
{
    Task<int> CreateSalaryAsync(SalaryRequest request, DateTime salaryDate);
    Task<SalaryResponse> GetSalaryByIdAsync(int salaryId);
    Task<ICollection<SalaryResponse>> GetSalariesAsync(Guid? employeeId, DateTime? from, DateTime? to);
    Task UpdateSalaryAsync(int salaryId, SalaryRequest request);
    Task DeleteSalaryAsync(int salaryId);
}

