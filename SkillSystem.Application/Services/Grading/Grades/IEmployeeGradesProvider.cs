using SkillSystem.Application.Services.Grading.Grades.Models;

namespace SkillSystem.Application.Services.Grading.Grades;

public interface IEmployeeGradesProvider
{
    Task<IReadOnlyCollection<EmployeeGradeResponse>> FindEmployeeGradesAsync(Guid employeeId, int? roleId = null);
    Task<EmployeeGradeResponse> GetRoleActualGradeAsync(Guid employeeId, int roleId);
}
