using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Skills;

public interface IEmployeeSkillsRepository
{
    Task AddEmployeeSkillsAsync(params EmployeeSkill[] employeeSkills);
    Task<EmployeeSkill?> FindEmployeeSkillAsync(string employeeId, int skillId);
    Task<EmployeeSkill> GetEmployeeSkillAsync(string employeeId, int skillId);
    Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(string employeeId, IEnumerable<int> skillsIds);
    Task UpdateSkillsAsync(params EmployeeSkill[] employeeSkills);
    Task DeleteEmployeeSkillsAsync(params EmployeeSkill[] skills);
}