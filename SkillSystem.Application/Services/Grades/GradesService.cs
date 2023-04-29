using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Application.Repositories.Positions;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.Grades.Models;
using SkillSystem.Application.Services.Positions.Models;
using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.Application.Services.Grades;

public class GradesService : IGradesService
{
    private readonly IGradesRepository gradesRepository;
    private readonly ISkillsRepository skillsRepository;
    private readonly IPositionsRepository positionsRepository;
    private readonly IUnitOfWork unitOfWork;

    public GradesService(
        IGradesRepository gradesRepository,
        ISkillsRepository skillsRepository,
        IPositionsRepository positionsRepository,
        IUnitOfWork unitOfWork)
    {
        this.gradesRepository = gradesRepository;
        this.skillsRepository = skillsRepository;
        this.positionsRepository = positionsRepository;
        this.unitOfWork = unitOfWork;
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

    public async Task<ICollection<PositionResponse>> GetGradePositionsAsync(int gradeId)
    {
        var positions = await gradesRepository.GetGradePositionsAsync(gradeId);
        return positions.Adapt<ICollection<PositionResponse>>();
    }

    public async Task UpdateGradeAsync(int gradeId, GradeRequest request)
    {
        var grade = await gradesRepository.GetGradeByIdAsync(gradeId);

        request.Adapt(grade);

        gradesRepository.UpdateGrade(grade);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AddGradeSkillAsync(int gradeId, int skillId)
    {
        var skill = await skillsRepository.GetSkillByIdAsync(skillId);
        await gradesRepository.AddGradeSkillAsync(gradeId, skill);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteGradeSkillAsync(int gradeId, int skillId)
    {
        await gradesRepository.DeleteGradeSkillAsync(gradeId, skillId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AddGradePositionAsync(int gradeId, int positionId)
    {
        var position = await positionsRepository.GetPositionByIdAsync(positionId);
        await gradesRepository.AddGradePositionAsync(gradeId, position);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteGradePositionAsync(int gradeId, int positionId)
    {
        await gradesRepository.DeleteGradePositionAsync(gradeId, positionId);
        await unitOfWork.SaveChangesAsync();
    }
}
