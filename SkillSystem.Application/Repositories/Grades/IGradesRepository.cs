using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Grades;

public interface IGradesRepository
{
    Task<Grade?> FindGradeByIdAsync(int gradeId);
    Task<Grade> GetGradeByIdAsync(int gradeId);
    IQueryable<Grade> FindGrades(string? title = default);
    Task<IReadOnlyCollection<Grade>> BatchGetGrades(IEnumerable<int> gradeIds);
    Task<IReadOnlyCollection<Grade>> GetGradesUntilAsync(int gradeId);
    Task<IEnumerable<Skill>> GetGradeSkillsAsync(int gradeId);
    Task<ICollection<Position>> GetGradePositionsAsync(int gradeId);
    void UpdateGrade(Grade grade);
    Task AddGradeSkillAsync(int gradeId, Skill skill);
    Task DeleteGradeSkillAsync(int gradeId, int skillId);
    Task AddGradePositionAsync(int gradeId, Position position);
    Task DeleteGradePositionAsync(int gradeId, int positionId);
}
