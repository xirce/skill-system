namespace SkillSystem.Application.Services.Grading.Grades;

public interface IEmployeeGradesService
{
    Task<EmployeeGradeChangeResult> AddEmployeeGradeAsync(Guid employeeId, int gradeId);
    Task<EmployeeGradeChangeResult> ApproveEmployeeGradeAsync(Guid employeeId, int gradeId);
}
