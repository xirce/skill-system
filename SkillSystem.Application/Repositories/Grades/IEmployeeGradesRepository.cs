using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Grades;

public interface IEmployeeGradesRepository
{
    Task AddGradesAsync(string employeeId, IEnumerable<Grade> grades);
    Task<EmployeeGrade?> FindEmployeeGradeAsync(string employeeId, int gradeId);
    Task<EmployeeGrade> GetEmployeeGradeAsync(string employeeId, int gradeId);
    Task<ICollection<EmployeeGrade>> GetEmployeeGradesAsync(string employeeId);
    Task<ICollection<EmployeeGrade>> FindEmployeeGradesAsync(string employeeId, IEnumerable<int> gradesIds);
    Task UpdateGrade(EmployeeGrade employeeGrade);
    Task DeleteGrade(EmployeeGrade employeeGrade);
}