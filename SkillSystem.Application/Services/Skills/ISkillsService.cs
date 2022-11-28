using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.Application.Services.Skills;

public interface ISkillsService
{
    Task<int> CreateSkillAsync(CreateSkillRequest request);
    Task<SkillResponse?> GetSkillByIdAsync(int skillId);
    Task<PaginatedResponse<SkillShortInfo>> FindSkillsAsync(SearchSkillsRequest request);
    Task<IEnumerable<SkillShortInfo>> GetSubSkillsAsync(int skillId);
    Task UpdateSkillAsync(int skillId, UpdateSkillRequest request);
    Task AttachSkillToGroupAsync(int skillId, int skillGroupId);
    Task DeleteSkillAsync(int skillId);
}