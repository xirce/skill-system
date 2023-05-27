namespace SkillSystem.Application.Services.Grading.Grades;

public interface IEmployeeGradingStrategy
{
    Task<EmployeeGradeChangeResult> GradeEmployee(Guid employeeId, int gradeId);
    Task<EmployeeGradeChangeResult> ApproveEmployeeGrade(Guid employeeId, int gradeId);
}
