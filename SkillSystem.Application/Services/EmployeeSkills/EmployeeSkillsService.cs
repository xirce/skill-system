using Mapster;
using SkillSystem.Application.Authorization;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Services;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.EmployeeSkills.Models;
using SkillSystem.Core.Entities;
using SkillSystem.Core.Enums;

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
        ICurrentUserProvider currentUserProvider)
    {
        this.employeeSkillsRepository = employeeSkillsRepository;
        this.skillsRepository = skillsRepository;
        this.rolesRepository = rolesRepository;
        this.currentUserProvider = currentUserProvider;
    }

    public async Task AddEmployeeSkillsAsync(string employeeId, IEnumerable<int> skillsIds)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var skillsToAdd = new List<EmployeeSkill>();
        foreach (var skillId in skillsIds)
            skillsToAdd.AddRange(await GetSkillsToAddAsync(employeeId, skillId));

        await employeeSkillsRepository.AddEmployeeSkillsAsync(skillsToAdd.ToArray());
    }

    public async Task<EmployeeSkillResponse> GetEmployeeSkillAsync(string employeeId, int skillId)
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
        string employeeId,
        int? roleId = null)
    {
        var skills = await FindEmployeeSkillsInternalAsync(employeeId, roleId);
        return skills.Adapt<ICollection<EmployeeSkillShortInfo>>();
    }

    public async Task ApproveSkillsAsync(string employeeId, IEnumerable<int> skillsIds)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var skillsToApprove = new List<EmployeeSkill>();
        foreach (var skillId in skillsIds)
            skillsToApprove.AddRange(await GetSkillsToApproveAsync(employeeId, skillId));

        foreach (var skill in skillsToApprove)
            skill.Status = EmployeeSkillStatus.Approved;

        await employeeSkillsRepository.UpdateSkillsAsync(skillsToApprove.ToArray());
    }

    public async Task DeleteEmployeeSkillsAsync(string employeeId, IEnumerable<int> skillsIds)
    {
        ThrowIfCurrentUserHasNotAccessTo(employeeId);

        var skillsToDelete = new List<EmployeeSkill>();
        foreach (var skillId in skillsIds)
            skillsToDelete.AddRange(await GetSkillsToDeleteAsync(employeeId, skillId));

        await employeeSkillsRepository.DeleteEmployeeSkillsAsync(skillsToDelete.ToArray());
    }

    private async Task<ICollection<EmployeeSkill>> FindEmployeeSkillsInternalAsync(string employeeId, int? roleId)
    {
        var skills = roleId.HasValue
            ? await FindEmployeeRoleSkillsAsync(employeeId, roleId.Value)
            : await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId);
        return skills;
    }

    private async Task<ICollection<EmployeeSkill>> GetSkillsToAddAsync(string employeeId, int skillId)
    {
        var skillWithSubSkills = (await skillsRepository.TraverseSkillAsync(skillId)).ToArray()
            .Select(subSkill => new EmployeeSkill { EmployeeId = employeeId, SkillId = subSkill.Id })
            .ToList();

        var addedSkillId = skillWithSubSkills.First().SkillId;
        var groupsToAdd = await skillsRepository.GetGroups(addedSkillId)
            .TakeWhileAwait(group => CanAddGroupAsync(employeeId, group.Id))
            .Select(skill => new EmployeeSkill { EmployeeId = employeeId, SkillId = skill.Id })
            .ToListAsync();

        var skillsToAdd = new List<EmployeeSkill>();
        skillsToAdd.AddRange(skillWithSubSkills);
        skillsToAdd.AddRange(groupsToAdd);

        return skillsToAdd;
    }

    private async Task<ICollection<EmployeeSkill>> GetSkillsToApproveAsync(string employeeId, int skillId)
    {
        var employeeSkill = await employeeSkillsRepository.GetEmployeeSkillAsync(employeeId, skillId);

        var subSkills = await GetSubSkillsToApproveAsync(employeeSkill);

        var groups = await GetGroupsToApproveAsync(employeeSkill);

        var skillsToApprove = new List<EmployeeSkill>();
        skillsToApprove.AddRange(subSkills);
        skillsToApprove.Add(employeeSkill);
        skillsToApprove.AddRange(groups);

        return skillsToApprove;
    }

    private async Task<List<EmployeeSkill>> GetSkillsToDeleteAsync(string employeeId, int skillId)
    {
        var employeeSkill = await employeeSkillsRepository.GetEmployeeSkillAsync(employeeId, skillId);

        var subSkills = await GetSubSkillsToDeleteAsync(employeeSkill);
        var groupsToDelete = await GetGroupsToDeleteAsync(employeeSkill);

        var skillsToDelete = new List<EmployeeSkill>();
        skillsToDelete.AddRange(subSkills);
        skillsToDelete.Add(employeeSkill);
        skillsToDelete.AddRange(groupsToDelete);
        return skillsToDelete;
    }

    private void ThrowIfCurrentUserHasNotAccessTo(string employeeId)
    {
        var currentUser = currentUserProvider.User;
        var currentUserId = currentUser?.GetUserId();
        if (currentUser is null || currentUserId != employeeId && !currentUser.IsInRole(AuthRoleNames.Admin))
            throw new ForbiddenException($"Access denied for user with id {currentUserId}");
    }

    private async ValueTask<bool> CanAddGroupAsync(string employeeId, int groupId)
    {
        var groupSkillsIds = await GetGroupSkillsIdsAsync(groupId);
        var foundEmployeeGroupSkills =
            await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, groupSkillsIds);
        return groupSkillsIds.Length - foundEmployeeGroupSkills.Count == 1;
    }

    private async Task<ICollection<EmployeeSkill>> GetGroupsToApproveAsync(EmployeeSkill employeeSkill)
    {
        var groups = skillsRepository.GetGroups(employeeSkill.SkillId)
            .TakeWhileAwait(group => CanApproveGroupAsync(employeeSkill.EmployeeId, group.Id));

        var groupsIds = await groups
            .Select(group => group.Id)
            .ToArrayAsync();

        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeSkill.EmployeeId, groupsIds);
    }

    private async Task<ICollection<EmployeeSkill>> GetSubSkillsToApproveAsync(EmployeeSkill employeeSkill)
    {
        var subSkillsIds = (await skillsRepository.TraverseSkillAsync(employeeSkill.SkillId))
            .Skip(1)
            .Select(subSkill => subSkill.Id)
            .ToArray();
        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeSkill.EmployeeId, subSkillsIds);
    }

    private async ValueTask<bool> CanApproveGroupAsync(string employeeId, int groupId)
    {
        var groupSkillsIds = await GetGroupSkillsIdsAsync(groupId);
        var approvedEmployeeSkillsCount =
            (await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, groupSkillsIds))
            .Count(skill => skill.Status == EmployeeSkillStatus.Approved);
        return groupSkillsIds.Length - approvedEmployeeSkillsCount == 1;
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
            .TakeWhileAwait(group => CanDeleteGroupAsync(employeeSkill.EmployeeId, group.Id))
            .Select(group => group.Id)
            .ToArrayAsync();
        return await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeSkill.EmployeeId, groupsIds);
    }

    private async ValueTask<bool> CanDeleteGroupAsync(string employeeId, int groupId)
    {
        var groupSkillsIds = await GetGroupSkillsIdsAsync(groupId);
        return (await employeeSkillsRepository.FindEmployeeSkillsAsync(employeeId, groupSkillsIds)).Count == 1;
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
