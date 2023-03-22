using Mapster;
using SkillSystem.Application.Repositories.Salaries;
using SkillSystem.Application.Services.Salaries.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Salaries;

public class SalariesService : ISalariesService
{
    private readonly ISalariesRepository salariesRepository;

    public SalariesService(ISalariesRepository salariesRepository)
    {
        this.salariesRepository = salariesRepository;
    }

    public async Task<int> CreateSalaryAsync(SalaryRequest request, DateTime salaryDate)
    {
        var salary = request.Adapt<Salary>();
        salary.SalaryDate = salaryDate;
        return await salariesRepository.CreateSalaryAsync(salary);
    }

    public async Task<SalaryResponse> GetSalaryByIdAsync(int salaryId)
    {
        var duty = await salariesRepository.GetSalaryByIdAsync(salaryId);
        return duty.Adapt<SalaryResponse>();
    }

    public async Task<ICollection<SalaryResponse>> GetSalariesAsync(Guid? employeeId, DateTime? from, DateTime? to)
    {
        var salaries = await salariesRepository.GetSalariesAsync(employeeId, from, to);
        var sortedSalaries = salaries.OrderBy(salary => salary.SalaryDate);
        return sortedSalaries.Adapt<ICollection<SalaryResponse>>();
    }

    public async Task UpdateSalaryAsync(int salaryId, SalaryRequest request)
    {
        var salary = await salariesRepository.GetSalaryByIdAsync(salaryId);

        request.Adapt(salary);

        await salariesRepository.UpdateSalaryAsync(salary);
    }

    public async Task DeleteSalaryAsync(int salaryId)
    {
        var salary = await salariesRepository.GetSalaryByIdAsync(salaryId);
        await salariesRepository.DeleteSalaryAsync(salary);
    }
}
