using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Skills;

public interface IEmployeeSkillsRepository : IEmployeeSkillsReadOnlyRepository
{
    Task AddEmployeeSkillsAsync(IEnumerable<EmployeeSkill> employeeSkills);
    void UpdateSkills(IEnumerable<EmployeeSkill> employeeSkills);
    void DeleteEmployeeSkills(IEnumerable<EmployeeSkill> skills);
}
