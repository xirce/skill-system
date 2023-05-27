using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Grading.Skills;

public class AutoChangeSubSkillsStrategy : IEmployeeSkillsChangeStrategy
{
    private readonly IEmployeeSkillsReadOnlyRepository employeeSkillsRepository;
    private readonly ISkillsReadOnlyRepository skillsRepository;

    public AutoChangeSubSkillsStrategy(
        IEmployeeSkillsReadOnlyRepository employeeSkillsRepository,
        ISkillsReadOnlyRepository skillsRepository)
    {
        this.employeeSkillsRepository = employeeSkillsRepository;
        this.skillsRepository = skillsRepository;
    }

    public async Task<EmployeeSkillsChangeResult> AddSkill(Guid employeeId, int skillId)
    {
        var skillIds = await TraverseSkillIds(skillId);
        return new EmployeeSkillsChangeResult(employeeId, skillIds);
    }

    public async Task<EmployeeSkillsChangeResult> ApproveSkill(Guid employeeId, int skillId)
    {
        await EnsureEmployeeHasSkill(employeeId, skillId);

        var employeeSkillIds = await TraverseEmployeeSkillIds(employeeId, skillId);

        return new EmployeeSkillsChangeResult(employeeId, employeeSkillIds);
    }

    public async Task<EmployeeSkillsChangeResult> DeleteSkill(Guid employeeId, int skillId)
    {
        await EnsureEmployeeHasSkill(employeeId, skillId);

        var employeeSkillIds = await TraverseEmployeeSkillIds(employeeId, skillId);

        return new EmployeeSkillsChangeResult(employeeId, employeeSkillIds);
    }

    private async Task<IReadOnlyCollection<int>> TraverseEmployeeSkillIds(Guid employeeId, int skillId)
    {
        var skillIds = await TraverseSkillIds(skillId);
        var employeeSkills = await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, skillIds);
        return employeeSkills
            .Select(employeeSkill => employeeSkill.SkillId)
            .ToArray();
    }

    private async Task<IReadOnlyCollection<int>> TraverseSkillIds(int skillId)
    {
        return (await skillsRepository.TraverseSkillAsync(skillId))
            .Select(skill => skill.Id)
            .ToArray();
    }

    private async Task EnsureEmployeeHasSkill(Guid employeeId, int skillId)
    {
        await employeeSkillsRepository.GetEmployeeSkillAsync(employeeId, skillId);
    }
}
