using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Grading.Grades;

public static class EmployeeGrade
{
    public static Core.Entities.EmployeeGrade InProgress(Guid employeeId, int gradeId)
    {
        return Create(employeeId, gradeId, EmployeeGradeStatus.InProgress);
    }

    public static Core.Entities.EmployeeGrade Achieved(Guid employeeId, int gradeId)
    {
        return Create(employeeId, gradeId, EmployeeGradeStatus.Achieved);
    }

    public static Core.Entities.EmployeeGrade Approved(Guid employeeId, int gradeId)
    {
        return Create(employeeId, gradeId, EmployeeGradeStatus.Approved);
    }

    private static Core.Entities.EmployeeGrade Create(Guid employeeId, int gradeId, EmployeeGradeStatus employeeGradeStatus)
    {
        return new Core.Entities.EmployeeGrade { EmployeeId = employeeId, GradeId = gradeId, Status = employeeGradeStatus };
    }
}
