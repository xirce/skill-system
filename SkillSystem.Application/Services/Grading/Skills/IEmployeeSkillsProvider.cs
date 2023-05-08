using SkillSystem.Application.Services.Grading.Skills.Models;

namespace SkillSystem.Application.Services.Grading.Skills;

public interface IEmployeeSkillsProvider
{
    Task<EmployeeSkillResponse> GetEmployeeSkillAsync(Guid employeeId, int skillId);
    Task<ICollection<EmployeeSkillShortInfo>> FindEmployeeSkillsAsync(Guid employeeId, int? roleId = null);
}
