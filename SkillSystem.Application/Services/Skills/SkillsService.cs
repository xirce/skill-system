using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.Skills.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Skills;

public class SkillsService : ISkillsService
{
    private readonly ISkillsRepository skillsRepository;
    private readonly IUnitOfWork unitOfWork;

    public SkillsService(ISkillsRepository skillsRepository, IUnitOfWork unitOfWork)
    {
        this.skillsRepository = skillsRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<int> CreateSkillAsync(CreateSkillRequest request)
    {
        if (request.GroupId.HasValue)
            await GetSkillByIdAsync(request.GroupId.Value);

        var skill = request.Adapt<Skill>();

        await skillsRepository.CreateSkillAsync(skill);
        await unitOfWork.SaveChangesAsync();
        return skill.Id;
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

        skillsRepository.UpdateSkill(skill);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AttachSkillToGroupAsync(int skillId, int skillGroupId)
    {
        var skill = await skillsRepository.GetSkillByIdAsync(skillId);
        var skillGroup = await skillsRepository.GetSkillByIdAsync(skillGroupId);

        skill.GroupId = skillGroup.Id;
        skillsRepository.UpdateSkill(skill);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteSkillAsync(int skillId)
    {
        var skill = await skillsRepository.GetSkillByIdAsync(skillId);
        skillsRepository.DeleteSkill(skill);
        await unitOfWork.SaveChangesAsync();
    }
}
