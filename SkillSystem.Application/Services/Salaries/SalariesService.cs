using Mapster;
using SkillSystem.Application.Repositories.Salaries;
using SkillSystem.Application.Services.Salaries.Models;

namespace SkillSystem.Application.Services.Salaries;

public class SalariesService : ISalariesService
{
    private readonly ISalariesRepository salariesRepository;

    public SalariesService(ISalariesRepository salariesRepository)
    {
        this.salariesRepository = salariesRepository;
    }

    public async Task<SalaryResponse> GetSalaryByIdAsync(int salaryId)
    {
        var salary = await salariesRepository.GetSalaryByIdAsync(salaryId);
        return salary.Adapt<SalaryResponse>();
    }

    public async Task<SalaryResponse> GetSalaryByMonthAsync(Guid employeeId, DateTime month)
    {
        var salary = await salariesRepository.GetSalaryByMonthAsync(employeeId, month);
        return salary.Adapt<SalaryResponse>();
    }

    public async Task<SalaryResponse> GetCurrentSalaryAsync(Guid employeeId)
    {
        var salary = await salariesRepository.GetSalaryByMonthAsync(employeeId, DateTime.UtcNow);
        return salary.Adapt<SalaryResponse>();
    }

    public async Task<ICollection<SalaryResponse>> GetSalariesAsync(Guid employeeId, DateTime? from, DateTime? to)
    {
        var salaries = await salariesRepository.GetSalariesAsync(employeeId, from, to);
        var sortedSalaries = salaries.OrderBy(salary => salary.StartDate);
        return sortedSalaries.Adapt<ICollection<SalaryResponse>>();
    }

    public async Task CancelSalaryAssigmentAsync(int salaryId)
    {
        var salary = await salariesRepository.GetSalaryByIdAsync(salaryId);
        if (salary.StartDate < DateTime.UtcNow || (salary.StartDate.Month == DateTime.UtcNow.Month &&
            salary.StartDate.Year == DateTime.UtcNow.Year))
            return;
        await salariesRepository.DeleteSalaryAsync(salary);
    }
}
