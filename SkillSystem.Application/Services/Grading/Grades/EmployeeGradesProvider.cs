using Mapster;
using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Application.Services.Grading.Grades.Models;

namespace SkillSystem.Application.Services.Grading.Grades;

public class EmployeeGradesProvider : IEmployeeGradesProvider
{
    private readonly IEmployeeGradesReadOnlyRepository employeeGradesRepository;

    public EmployeeGradesProvider(IEmployeeGradesReadOnlyRepository employeeGradesRepository)
    {
        this.employeeGradesRepository = employeeGradesRepository;
    }

    public async Task<IReadOnlyCollection<EmployeeGradeResponse>> FindEmployeeGradesAsync(
        Guid employeeId,
        int? roleId = null)
    {
        var employeeGrades = await employeeGradesRepository.FindEmployeeGrades(employeeId, roleId);
        return employeeGrades.Adapt<IReadOnlyCollection<EmployeeGradeResponse>>();
    }

    public async Task<EmployeeGradeResponse> GetRoleActualGradeAsync(Guid employeeId, int roleId)
    {
        var lastEmployeeGrade = await employeeGradesRepository.GetLastRoleGradeAsync(employeeId, roleId);
        return lastEmployeeGrade.Adapt<EmployeeGradeResponse>();
    }
}
