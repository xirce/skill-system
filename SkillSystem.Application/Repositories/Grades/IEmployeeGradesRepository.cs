using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Grades;

public interface IEmployeeGradesRepository : IEmployeeGradesReadOnlyRepository
{
    Task AddGradesAsync(IEnumerable<EmployeeGrade> grades);
    void UpdateGrades(IEnumerable<EmployeeGrade> grades);
    void DeleteGrades(IEnumerable<EmployeeGrade> grades);
}
