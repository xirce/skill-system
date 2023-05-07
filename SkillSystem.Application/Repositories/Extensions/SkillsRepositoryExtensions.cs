using SkillSystem.Application.Repositories.Skills;

namespace SkillSystem.Application.Repositories.Extensions;

public static class SkillsRepositoryExtensions
{
    public static async Task<int[]> GetGroupSkillsIdsAsync(this ISkillsReadOnlyRepository skillsRepository, int groupId)
    {
        return (await skillsRepository.GetSubSkillsAsync(groupId))
            .Select(skill => skill.Id)
            .ToArray();
    }
}
