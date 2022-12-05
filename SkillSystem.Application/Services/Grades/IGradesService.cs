using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Services.Grades.Models;
using SkillSystem.Application.Services.Skills.Models;

namespace SkillSystem.Application.Services.Grades;

public interface IGradesService
{
    Task<GradeResponse> GetGradeByIdAsync(int gradeId);
    Task<PaginatedResponse<GradeShortInfo>> FindGradesAsync(SearchGradesRequest request);
    Task<IEnumerable<SkillResponse>> GetGradeSkillsAsync(int gradeId);
    Task UpdateGradeAsync(int gradeId, GradeRequest request);
    Task AddGradeSkillAsync(int gradeId, int skillId);
    Task DeleteGradeSkillAsync(int gradeId, int skillId);
}