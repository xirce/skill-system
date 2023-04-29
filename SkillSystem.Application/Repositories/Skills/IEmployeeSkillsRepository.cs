using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Skills;

public interface IEmployeeSkillsRepository
{
    Task AddEmployeeSkillsAsync(IEnumerable<EmployeeSkill> employeeSkills);
    Task<EmployeeSkill?> FindEmployeeSkillAsync(string employeeId, int skillId);
    Task<EmployeeSkill> GetEmployeeSkillAsync(string employeeId, int skillId);
    Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(string employeeId);
    Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(string employeeId, IEnumerable<int> skillsIds);
    void UpdateSkills(IEnumerable<EmployeeSkill> employeeSkills);
    void DeleteEmployeeSkills(IEnumerable<EmployeeSkill> skills);
}
