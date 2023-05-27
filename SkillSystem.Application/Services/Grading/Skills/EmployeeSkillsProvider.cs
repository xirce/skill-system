using Mapster;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.Grading.Skills.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Grading.Skills;

public class EmployeeSkillsProvider : IEmployeeSkillsProvider
{
    private readonly IEmployeeSkillsReadOnlyRepository employeeSkillsRepository;
    private readonly ISkillsReadOnlyRepository skillsRepository;
    private readonly IRolesRepository rolesRepository;

    public EmployeeSkillsProvider(
        IEmployeeSkillsReadOnlyRepository employeeSkillsRepository,
        ISkillsReadOnlyRepository skillsRepository,
        IRolesRepository rolesRepository)
    {
        this.employeeSkillsRepository = employeeSkillsRepository;
        this.skillsRepository = skillsRepository;
        this.rolesRepository = rolesRepository;
    }

    public async Task<EmployeeSkillResponse> GetEmployeeSkillAsync(Guid employeeId, int skillId)
    {
        var skill = await employeeSkillsRepository.GetEmployeeSkillAsync(employeeId, skillId);
        var subSkillsIds = (await skillsRepository.GetSubSkillsAsync(skillId))
            .Select(subSkill => subSkill.Id)
            .ToArray();
        var subSkills = await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, subSkillsIds);
        var mappedSubSkills = subSkills.Adapt<ICollection<EmployeeSkillShortInfo>>();

        return skill.Adapt<EmployeeSkillResponse>() with { SubSkills = mappedSubSkills };
    }

    public async Task<ICollection<EmployeeSkillShortInfo>> FindEmployeeSkillsAsync(
        Guid employeeId,
        int? roleId = null)
    {
        var skills = await FindEmployeeSkillsInternalAsync(employeeId, roleId);
        return skills.Adapt<ICollection<EmployeeSkillShortInfo>>();
    }


    private async Task<ICollection<EmployeeSkill>> FindEmployeeSkillsInternalAsync(Guid employeeId, int? roleId)
    {
        var skills = roleId.HasValue
            ? await FindEmployeeRoleSkillsAsync(employeeId, roleId.Value)
            : await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId);
        return skills;
    }

    private async Task<ICollection<EmployeeSkill>> FindEmployeeRoleSkillsAsync(Guid employeeId, int roleId)
    {
        var roleGrades = await rolesRepository.GetRoleGradesAsync(roleId, true);

        var skillsIds = roleGrades
            .SelectMany(grade => grade.Skills.Select(skill => skill.Id))
            .ToHashSet();

        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, skillsIds);
    }
}
