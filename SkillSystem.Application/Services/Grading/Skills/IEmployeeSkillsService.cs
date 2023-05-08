namespace SkillSystem.Application.Services.Grading.Skills;

public interface IEmployeeSkillsService
{
    Task<EmployeeSkillsChangeResult> AddEmployeeSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds);
    Task<EmployeeSkillsChangeResult> ApproveSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds);
    Task<EmployeeSkillsChangeResult> DeleteEmployeeSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds);
}
