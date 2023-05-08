using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Grading.Skills;

public class EmployeeSkillsService : IEmployeeSkillsService
{
    private readonly IEmployeeSkillsRepository employeeSkillsRepository;
    private readonly IEmployeeSkillsChangeStrategy employeeSkillsChangeStrategy;
    private readonly IUnitOfWork unitOfWork;

    public EmployeeSkillsService(
        IEmployeeSkillsRepository employeeSkillsRepository,
        IEmployeeSkillsChangeStrategy employeeSkillsChangeStrategy,
        IUnitOfWork unitOfWork)
    {
        this.employeeSkillsRepository = employeeSkillsRepository;
        this.employeeSkillsChangeStrategy = employeeSkillsChangeStrategy;
        this.unitOfWork = unitOfWork;
    }

    public async Task<EmployeeSkillsChangeResult> AddEmployeeSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds)
    {
        var skillsToAddIds = new HashSet<int>();
        foreach (var skillId in skillsIds)
        {
            var addSkillResult = await employeeSkillsChangeStrategy.AddSkill(employeeId, skillId);
            skillsToAddIds.UnionWith(addSkillResult.AffectedSkillIds);
        }

        var skillsToAdd = skillsToAddIds
            .Select(nextSkillId => EmployeeSkills.Received(employeeId, nextSkillId))
            .ToArray();

        await employeeSkillsRepository.AddEmployeeSkillsAsync(skillsToAdd);
        await unitOfWork.SaveChangesAsync();

        return new EmployeeSkillsChangeResult(employeeId, skillsToAddIds);
    }

    public async Task<EmployeeSkillsChangeResult> ApproveSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds)
    {
        var skillsToApproveIds = new HashSet<int>();
        foreach (var skillId in skillsIds)
        {
            var approveSkillResult = await employeeSkillsChangeStrategy.ApproveSkill(employeeId, skillId);
            skillsToApproveIds.UnionWith(approveSkillResult.AffectedSkillIds);
        }

        var skillsToApprove = skillsToApproveIds
            .Select(skillId => EmployeeSkills.Approved(employeeId, skillId))
            .ToArray();

        employeeSkillsRepository.UpdateSkills(skillsToApprove);
        await unitOfWork.SaveChangesAsync();

        return new EmployeeSkillsChangeResult(employeeId, skillsToApproveIds);
    }

    public async Task<EmployeeSkillsChangeResult> DeleteEmployeeSkillsAsync(Guid employeeId, IEnumerable<int> skillsIds)
    {
        var skillsToDeleteIds = new HashSet<int>();
        foreach (var skillId in skillsIds)
        {
            var deleteSkillResult = await employeeSkillsChangeStrategy.DeleteSkill(employeeId, skillId);
            skillsToDeleteIds.UnionWith(deleteSkillResult.AffectedSkillIds);
        }

        var skillsToDelete = await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, skillsToDeleteIds);

        employeeSkillsRepository.DeleteEmployeeSkills(skillsToDelete);
        await unitOfWork.SaveChangesAsync();

        return new EmployeeSkillsChangeResult(employeeId, skillsToDeleteIds);
    }
}
