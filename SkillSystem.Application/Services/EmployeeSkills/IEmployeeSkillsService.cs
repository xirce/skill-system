using SkillSystem.Application.Services.EmployeeSkills.Models;

namespace SkillSystem.Application.Services.EmployeeSkills;

public interface IEmployeeSkillsService
{
    Task AddEmployeeSkillAsync(string employeeId, int skillId);
    Task<EmployeeSkillResponse> GetEmployeeSkillAsync(string employeeId, int skillId);
    Task<ICollection<EmployeeSkillShortInfo>> FindEmployeeSkillsAsync(string employeeId, int roleId);
    Task<ICollection<EmployeeSkillStatus>> FindEmployeeSkillsStatusesAsync(string employeeId, int roleId);
    Task SetSkillApprovedAsync(string employeeId, int skillId, bool isApproved);
    Task DeleteEmployeeSkillAsync(string employeeId, int skillId);
}