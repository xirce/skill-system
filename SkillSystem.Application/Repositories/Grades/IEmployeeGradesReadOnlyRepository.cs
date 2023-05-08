using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Grades;

public interface IEmployeeGradesReadOnlyRepository
{
    Task<EmployeeGrade?> FindEmployeeGradeAsync(Guid employeeId, int gradeId);
    Task<EmployeeGrade> GetEmployeeGradeAsync(Guid employeeId, int gradeId);
    Task<IReadOnlyCollection<EmployeeGrade>> FindEmployeeGrades(Guid employeeId, int? roleId = null);
    Task<EmployeeGrade?> FindLastRoleGradeAsync(Guid employeeId, int roleId);
    Task<EmployeeGrade> GetLastRoleGradeAsync(Guid employeeId, int roleId);
    Task<IReadOnlyCollection<EmployeeGrade>> FindEmployeeGradesAsync(Guid employeeId, IEnumerable<int> gradeIds);
}
