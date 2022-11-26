using Mapster;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.Skills.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Skills;

public class SkillsService : ISkillsService
{
    private readonly ISkillsRepository skillsRepository;

    public SkillsService(ISkillsRepository skillsRepository)
    {
        this.skillsRepository = skillsRepository;
    }

    public async Task<int> CreateSkillAsync(CreateSkillRequest request)
    {
        var skill = request.Adapt<Skill>();

        await skillsRepository.CreateSkillAsync(skill);

        return skill.Id;
    }

    public async Task<SkillResponse?> FindSkillByIdAsync(int skillId)
    {
        var skill = await skillsRepository.FindSkillByIdAsync(skillId, true);

        if (skill is null)
            throw new EntityNotFoundException(nameof(Skill), skillId);

        return skill.Adapt<SkillResponse>();
    }

    public Task<PaginatedResponse<SkillShortInfo>> FindSkillsAsync(SearchSkillsRequest request)
    {
        var skills = skillsRepository.FindSkills(request.Title);

        var paginatedSkills = skills
            .ProjectToType<SkillShortInfo>()
            .ToPaginatedList(request)
            .ToResponse();

        return Task.FromResult(paginatedSkills);
    }

    public async Task<IEnumerable<SkillShortInfo>> GetSubSkillsAsync(int skillId)
    {
        var skill = await skillsRepository.FindSkillByIdAsync(skillId, true);

        if (skill is null)
            throw new EntityNotFoundException(nameof(Skill), skillId);

        return skill.SubSkills.Adapt<IEnumerable<SkillShortInfo>>();
    }

    public async Task UpdateSkillAsync(int skillId, UpdateSkillRequest request)
    {
        var skill = await skillsRepository.FindSkillByIdAsync(skillId);

        if (skill is null)
            throw new EntityNotFoundException(nameof(Skill), skillId);

        request.Adapt(skill);

        await skillsRepository.UpdateSkillAsync(skill);
    }

    public async Task AttachSkillToGroupAsync(int skillId, int skillGroupId)
    {
        var skill = await skillsRepository.FindSkillByIdAsync(skillId);

        if (skill is null)
            throw new EntityNotFoundException(nameof(Skill), skillId);

        var skillGroup = await skillsRepository.FindSkillByIdAsync(skillGroupId);

        if (skillGroup is null)
            throw new EntityNotFoundException(nameof(Skill), skillGroupId);

        skill.GroupId = skillGroupId;
        await skillsRepository.UpdateSkillAsync(skill);
    }

    public async Task DeleteSkillAsync(int skillId)
    {
        await skillsRepository.DeleteSkillAsync(skillId);
    }
}