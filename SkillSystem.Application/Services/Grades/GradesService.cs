using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.Grades.Models;
using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.Application.Services.Grades;

public class GradesService : IGradesService
{
    private readonly IGradesRepository gradesRepository;
    private readonly ISkillsRepository skillsRepository;

    public GradesService(
        IGradesRepository gradesRepository,
        ISkillsRepository skillsRepository
    )
    {
        this.gradesRepository = gradesRepository;
        this.skillsRepository = skillsRepository;
    }

    public async Task<GradeResponse> GetGradeByIdAsync(int gradeId)
    {
        var grade = await gradesRepository.GetGradeByIdAsync(gradeId);
        return grade.Adapt<GradeResponse>();
    }

    public Task<PaginatedResponse<GradeShortInfo>> FindGradesAsync(SearchGradesRequest request)
    {
        var grades = gradesRepository.FindGrades(request.Title);

        var paginatedGrades = grades
            .ProjectToType<GradeShortInfo>()
            .ToPaginatedList(request)
            .ToResponse();

        return Task.FromResult(paginatedGrades);
    }

    public async Task<IEnumerable<SkillResponse>> GetGradeSkillsAsync(int gradeId)
    {
        var skills = await gradesRepository.GetGradeSkillsAsync(gradeId);
        return skills.Adapt<IEnumerable<SkillResponse>>();
    }

    public async Task UpdateGradeAsync(int gradeId, GradeRequest request)
    {
        var grade = await gradesRepository.GetGradeByIdAsync(gradeId);

        request.Adapt(grade);

        await gradesRepository.UpdateGradeAsync(grade);
    }

    public async Task AddGradeSkillAsync(int gradeId, int skillId)
    {
        var skill = await skillsRepository.GetSkillByIdAsync(skillId);
        await gradesRepository.AddGradeSkillAsync(gradeId, skill);
    }

    public async Task DeleteGradeSkillAsync(int gradeId, int skillId)
    {
        await gradesRepository.DeleteGradeSkillAsync(gradeId, skillId);
    }
}