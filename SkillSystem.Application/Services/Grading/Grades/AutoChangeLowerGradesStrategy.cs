using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Grading.Grades;

public class AutoChangeLowerGradesStrategy : IEmployeeGradingStrategy
{
    private readonly IGradesRepository gradesRepository;
    private readonly IEmployeeGradesReadOnlyRepository employeeGradesRepository;

    public AutoChangeLowerGradesStrategy(
        IGradesRepository gradesRepository,
        IEmployeeGradesReadOnlyRepository employeeGradesRepository)
    {
        this.gradesRepository = gradesRepository;
        this.employeeGradesRepository = employeeGradesRepository;
    }

    public async Task<EmployeeGradeChangeResult> GradeEmployee(Guid employeeId, int gradeId)
    {
        var gradeToAdd = await gradesRepository.GetGradeByIdAsync(gradeId);
        var unachievedGrades = await GetUnachievedGradesAsync(employeeId, gradeToAdd);

        var gradesToAddIds = unachievedGrades
            .Append(gradeToAdd)
            .Select(grade => grade.Id)
            .ToArray();

        return new EmployeeGradeChangeResult(employeeId, gradesToAddIds);
    }

    public async Task<EmployeeGradeChangeResult> ApproveEmployeeGrade(Guid employeeId, int gradeId)
    {
        var grades = await gradesRepository.GetGradesUntilAsync(gradeId);
        var lowerGradeIds = grades
            .SkipLast(1)
            .Select(nextGrade => nextGrade.Id);
        var employeeLowerGrades = await employeeGradesRepository.FindEmployeeGradesAsync(employeeId, lowerGradeIds);
        var gradesToApproveIds = employeeLowerGrades
            .Select(grade => grade.GradeId)
            .Append(gradeId)
            .ToArray();

        return new EmployeeGradeChangeResult(employeeId, gradesToApproveIds);
    }

    private async Task<IReadOnlyCollection<Grade>> GetUnachievedGradesAsync(Guid employeeId, Grade untilGrade)
    {
        var lastEmployeeGrade = await employeeGradesRepository.FindLastRoleGradeAsync(employeeId, untilGrade.RoleId);
        var gradesBefore = await gradesRepository.GetGradesUntilAsync(untilGrade.Id);

        return lastEmployeeGrade is not null
            ? gradesBefore
                .SkipWhile(nextGrade => nextGrade.Id != lastEmployeeGrade.GradeId)
                .Skip(1)
                .ToArray()
            : gradesBefore;
    }
}
