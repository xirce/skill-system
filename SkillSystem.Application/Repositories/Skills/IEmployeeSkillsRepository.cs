using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Skills;

public interface IEmployeeSkillsRepository
{
    Task AddEmployeeSkillsAsync(IEnumerable<EmployeeSkill> employeeSkills);
    Task<EmployeeSkill?> FindEmployeeSkillAsync(Guid employeeId, int skillId);
    Task<EmployeeSkill> GetEmployeeSkillAsync(Guid employeeId, int skillId);
    Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(Guid employeeId);
    Task<ICollection<EmployeeSkill>> FindEmployeeSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds);
    void UpdateSkills(IEnumerable<EmployeeSkill> employeeSkills);
    void DeleteEmployeeSkills(IEnumerable<EmployeeSkill> skills);
}
