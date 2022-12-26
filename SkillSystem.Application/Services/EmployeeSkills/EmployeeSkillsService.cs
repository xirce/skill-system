using Mapster;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Services;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.EmployeeSkills.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.EmployeeSkills;

public class EmployeeSkillsService : IEmployeeSkillsService
{
    private readonly IEmployeeSkillsRepository employeeSkillsRepository;
    private readonly ISkillsRepository skillsRepository;
    private readonly IRolesRepository rolesRepository;
    private readonly ICurrentUserProvider currentUserProvider;

    public EmployeeSkillsService(
        IEmployeeSkillsRepository employeeSkillsRepository,
        ISkillsRepository skillsRepository,
        IRolesRepository rolesRepository,
        ICurrentUserProvider currentUserProvider
    )
    {
        this.employeeSkillsRepository = employeeSkillsRepository;
        this.skillsRepository = skillsRepository;
        this.rolesRepository = rolesRepository;
        this.currentUserProvider = currentUserProvider;
    }

    public async Task AddEmployeeSkillAsync(string employeeId, int skillId)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var skillWithSubSkills = (await skillsRepository.TraverseSkillAsync(skillId)).ToArray()
            .Select(subSkill => new EmployeeSkill { EmployeeId = employeeId, SkillId = subSkill.Id })
            .ToList();

        var addedSkillId = skillWithSubSkills.First().SkillId;
        var groupsToAdd = await skillsRepository.GetGroups(addedSkillId)
            .TakeWhile(group => CountSkillsToAddGroupAsync(employeeId, group.Id).GetAwaiter().GetResult() < 2)
            .Select(skill => new EmployeeSkill { EmployeeId = employeeId, SkillId = skill.Id })
            .ToListAsync();

        var skillsToAdd = new List<EmployeeSkill>();
        skillsToAdd.AddRange(skillWithSubSkills);
        skillsToAdd.AddRange(groupsToAdd);

        await employeeSkillsRepository.AddEmployeeSkillsAsync(skillsToAdd.ToArray());
    }

    public async Task<EmployeeSkillResponse> GetEmployeeSkillAsync(string employeeId, int skillId)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var skill = await employeeSkillsRepository.GetEmployeeSkillAsync(employeeId, skillId);
        var subSkillsIds = (await skillsRepository.GetSubSkillsAsync(skillId))
            .Select(subSkill => subSkill.Id)
            .ToArray();
        var subSkills = await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, subSkillsIds);
        var mappedSubSkills = subSkills.Adapt<ICollection<EmployeeSkillShortInfo>>();

        return skill.Adapt<EmployeeSkillResponse>() with { SubSkills = mappedSubSkills };
    }

    public async Task<ICollection<EmployeeSkillShortInfo>> FindEmployeeSkillsAsync(string employeeId, int roleId)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var skills = await FindEmployeeRoleSkillsAsync(employeeId, roleId);
        return skills.Adapt<ICollection<EmployeeSkillShortInfo>>();
    }

    public async Task<ICollection<EmployeeSkillStatus>> FindEmployeeSkillsStatusesAsync(string employeeId, int roleId)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var skills = await FindEmployeeRoleSkillsAsync(employeeId, roleId);
        return skills.Adapt<ICollection<EmployeeSkillStatus>>();
    }

    public async Task SetSkillApprovedAsync(string employeeId, int skillId, bool isApproved)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var employeeSkill = await employeeSkillsRepository.GetEmployeeSkillAsync(employeeId, skillId);

        if (employeeSkill.IsApproved == isApproved)
            return;

        var subSkills = await GetSubSkillsToSetApprovedAsync(employeeSkill);
        var groups = await GetGroupsToSetApprovedAsync(employeeSkill, isApproved);

        var skillsToUpdate = new List<EmployeeSkill>();
        skillsToUpdate.AddRange(subSkills);
        skillsToUpdate.Add(employeeSkill);
        skillsToUpdate.AddRange(groups);

        foreach (var skill in skillsToUpdate)
            skill.IsApproved = isApproved;

        await employeeSkillsRepository.UpdateSkillsAsync(skillsToUpdate.ToArray());
    }

    public async Task DeleteEmployeeSkillAsync(string employeeId, int skillId)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var employeeSkill = await employeeSkillsRepository.FindEmployeeSkillAsync(employeeId, skillId);
        if (employeeSkill is null)
            return;

        var subSkills = await GetSubSkillsToDeleteAsync(employeeSkill);
        var groupsToDelete = await GetGroupsToDeleteAsync(employeeSkill);

        var skillsToDelete = new List<EmployeeSkill>();
        skillsToDelete.AddRange(subSkills);
        skillsToDelete.Add(employeeSkill);
        skillsToDelete.AddRange(groupsToDelete);

        await employeeSkillsRepository.DeleteEmployeeSkillsAsync(skillsToDelete.ToArray());
    }

    private void ThrowIfCurrentUserHasNotAccessTo(string employeeId)
    {
        var currentUserId = currentUserProvider.User?.GetUserId();
        if (currentUserId != employeeId)
            throw new ForbiddenException($"Access denied for user with id {currentUserId}");
    }

    private async Task<int> CountSkillsToAddGroupAsync(string employeeId, int groupId)
    {
        var groupSkillsIds = await GetGroupSkillsIdsAsync(groupId);
        var foundEmployeeGroupSkills =
            await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, groupSkillsIds);
        return groupSkillsIds.Length - foundEmployeeGroupSkills.Count;
    }

    private async Task<ICollection<EmployeeSkill>> GetGroupsToSetApprovedAsync(
        EmployeeSkill employeeSkill,
        bool toApprove
    )
    {
        var groups = skillsRepository.GetGroups(employeeSkill.SkillId);

        if (toApprove)
            groups = groups.TakeWhile(
                group => CountSkillsToApproveGroupAsync(employeeSkill.EmployeeId, group.Id).GetAwaiter().GetResult() < 2
            );

        var groupsIds = await groups
            .Select(group => group.Id)
            .ToArrayAsync();

        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeSkill.EmployeeId, groupsIds);
    }

    private async Task<ICollection<EmployeeSkill>> GetSubSkillsToSetApprovedAsync(EmployeeSkill employeeSkill)
    {
        var subSkillsIds = (await skillsRepository.TraverseSkillAsync(employeeSkill.SkillId))
            .Skip(1)
            .Select(subSkill => subSkill.Id)
            .ToArray();
        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeSkill.EmployeeId, subSkillsIds);
    }

    private async Task<int> CountSkillsToApproveGroupAsync(string employeeId, int groupId)
    {
        var groupSkillsIds = await GetGroupSkillsIdsAsync(groupId);
        var approvedEmployeeSkillsCount =
            (await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, groupSkillsIds))
            .Count(skill => skill.IsApproved);
        return groupSkillsIds.Length - approvedEmployeeSkillsCount;
    }

    private async Task<ICollection<EmployeeSkill>> GetSubSkillsToDeleteAsync(EmployeeSkill employeeSkill)
    {
        var subSkillsIds = (await skillsRepository.TraverseSkillAsync(employeeSkill.SkillId))
            .Skip(1)
            .Select(skill => skill.Id)
            .ToArray();
        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeSkill.EmployeeId, subSkillsIds);
    }

    private async Task<ICollection<EmployeeSkill>> GetGroupsToDeleteAsync(EmployeeSkill employeeSkill)
    {
        var groupsIds = await skillsRepository.GetGroups(employeeSkill.SkillId)
            .TakeWhile(group => CountGroupSkillsAsync(employeeSkill.EmployeeId, group.Id).GetAwaiter().GetResult() < 2)
            .Select(group => group.Id)
            .ToArrayAsync();
        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeSkill.EmployeeId, groupsIds);
    }

    private async Task<int> CountGroupSkillsAsync(string employeeId, int groupId)
    {
        var groupSkillsIds = await GetGroupSkillsIdsAsync(groupId);
        return (await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, groupSkillsIds)).Count;
    }

    private async Task<int[]> GetGroupSkillsIdsAsync(int groupId)
    {
        return (await skillsRepository.GetSubSkillsAsync(groupId))
            .Select(skill => skill.Id)
            .ToArray();
    }

    private async Task<ICollection<EmployeeSkill>> FindEmployeeRoleSkillsAsync(string employeeId, int roleId)
    {
        var roleGrades = await rolesRepository.GetRoleGradesAsync(roleId, true);

        var skillsIds = roleGrades
            .SelectMany(grade => grade.Skills.Select(skill => skill.Id))
            .ToHashSet();

        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, skillsIds);
    }
}