using SkillSystem.Application.Services.EmployeeSkills.Models;

namespace SkillSystem.Application.Services.EmployeeSkills;

public interface IEmployeeSkillsService
{
    Task AddEmployeeSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds);
    Task<EmployeeSkillResponse> GetEmployeeSkillAsync(Guid employeeId, int skillId);
    Task<ICollection<EmployeeSkillShortInfo>> FindEmployeeSkillsAsync(Guid employeeId, int? roleId = null);
    Task ApproveSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds);
    Task DeleteEmployeeSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds);
}
