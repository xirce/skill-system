using SkillSystem.Application.Services.EmployeeSkills.Models;

namespace SkillSystem.Application.Services.EmployeeSkills;

public interface IEmployeeSkillsService
{
    Task AddEmployeeSkillsAsync(string employeeId, IEnumerable<int> skillsIds);
    Task<EmployeeSkillResponse> GetEmployeeSkillAsync(string employeeId, int skillId);
    Task<ICollection<EmployeeSkillShortInfo>> FindEmployeeSkillsAsync(string employeeId, int? roleId = null);
    Task<ICollection<EmployeeSkillStatus>> FindEmployeeSkillsStatusesAsync(string employeeId, int? roleId = null);
    Task SetApprovedToSkillsAsync(string employeeId, bool isApproved, IEnumerable<int> skillsIds);
    Task DeleteEmployeeSkillsAsync(string employeeId, IEnumerable<int> skillsIds);
}
