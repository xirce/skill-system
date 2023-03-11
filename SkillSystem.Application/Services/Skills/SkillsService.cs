using Mapster;
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

        return await skillsRepository.CreateSkillAsync(skill);
    }

    public async Task<SkillResponse?> GetSkillByIdAsync(int skillId)
    {
        var skill = await skillsRepository.GetSkillByIdAsync(skillId, true);
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
        var subSkills = await skillsRepository.GetSubSkillsAsync(skillId);
        return subSkills.Adapt<IEnumerable<SkillShortInfo>>();
    }

    public async Task UpdateSkillAsync(int skillId, UpdateSkillRequest request)
    {
        var skill = await skillsRepository.GetSkillByIdAsync(skillId);

        request.Adapt(skill);

        await skillsRepository.UpdateSkillAsync(skill);
    }

    public async Task AttachSkillToGroupAsync(int skillId, int skillGroupId)
    {
        var skill = await skillsRepository.GetSkillByIdAsync(skillId);
        var skillGroup = await skillsRepository.GetSkillByIdAsync(skillGroupId);

        skill.GroupId = skillGroup.Id;
        await skillsRepository.UpdateSkillAsync(skill);
    }

    public async Task DeleteSkillAsync(int skillId)
    {
        var skill = await skillsRepository.GetSkillByIdAsync(skillId);
        await skillsRepository.DeleteSkillAsync(skill);
    }
}
