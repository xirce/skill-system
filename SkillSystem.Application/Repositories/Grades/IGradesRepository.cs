using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Grades;

public interface IGradesRepository
{
    Task<Grade?> FindGradeByIdAsync(int gradeId);
    Task<Grade> GetGradeByIdAsync(int gradeId);
    IQueryable<Grade> FindGrades(string? title = default);
    Task<IEnumerable<Skill>> GetGradeSkillsAsync(int gradeId);
    Task UpdateGradeAsync(Grade grade);
    Task AddGradeSkillAsync(int gradeId, Skill skill);
    Task DeleteGradeSkillAsync(int gradeId, int skillId);
}