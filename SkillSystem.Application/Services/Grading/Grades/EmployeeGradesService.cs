using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Application.Services.Grading.Skills;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Grading.Grades;

public class EmployeeGradesService : IEmployeeGradesService
{
    private readonly IEmployeeGradingStrategy employeeGradingStrategy;
    private readonly IEmployeeGradesRepository employeeGradesRepository;
    private readonly IGradesRepository gradesRepository;
    private readonly IEmployeeSkillsService employeeSkillsService;
    private readonly IUnitOfWork unitOfWork;

    public EmployeeGradesService(
        IEmployeeGradingStrategy employeeGradingStrategy,
        IEmployeeGradesRepository employeeGradesRepository,
        IGradesRepository gradesRepository,
        IEmployeeSkillsService employeeSkillsService,
        IUnitOfWork unitOfWork)
    {
        this.employeeGradingStrategy = employeeGradingStrategy;
        this.employeeGradesRepository = employeeGradesRepository;
        this.gradesRepository = gradesRepository;
        this.employeeSkillsService = employeeSkillsService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<EmployeeGradeChangeResult> AddEmployeeGradeAsync(Guid employeeId, int gradeId)
    {
        var gradeEmployeeResult = await employeeGradingStrategy.GradeEmployee(employeeId, gradeId);
        var gradesToAdd = await gradesRepository.BatchGetGrades(gradeEmployeeResult.AffectedGradeIds);

        var skillsToAddIds = gradesToAdd.SelectMany(grade => grade.Skills.Select(skill => skill.Id));

        var employeeGradesToAdd = gradesToAdd.Select(grade => EmployeeGrade.Achieved(employeeId, grade.Id));

        await employeeSkillsService.AddEmployeeSkillsAsync(employeeId, skillsToAddIds);
        await employeeGradesRepository.AddGradesAsync(employeeGradesToAdd);
        await unitOfWork.SaveChangesAsync();

        return gradeEmployeeResult;
    }

    public async Task<EmployeeGradeChangeResult> ApproveEmployeeGradeAsync(Guid employeeId, int gradeId)
    {
        var employeeGrade = await employeeGradesRepository.GetEmployeeGradeAsync(employeeId, gradeId);

        if (employeeGrade.Status == EmployeeGradeStatus.InProgress)
            throw new InvalidOperationException(
                $"Employee grade must have status {EmployeeGradeStatus.Achieved.ToString()} to approve it");

        var approveGradeResult = await employeeGradingStrategy.ApproveEmployeeGrade(employeeId, employeeGrade.GradeId);
        var employeeGradesToApprove = await employeeGradesRepository.FindEmployeeGradesAsync(
            employeeId, approveGradeResult.AffectedGradeIds);

        foreach (var gradeToApprove in employeeGradesToApprove)
            gradeToApprove.Status = EmployeeGradeStatus.Approved;

        var gradeIds = employeeGradesToApprove.Select(grade => grade.GradeId);
        var grades = await gradesRepository.BatchGetGrades(gradeIds);
        var skillsToApprove = grades
            .SelectMany(grade => grade.Skills.Select(skill => skill.Id));

        await employeeSkillsService.ApproveSkillsAsync(employeeId, skillsToApprove);
        employeeGradesRepository.UpdateGrades(employeeGradesToApprove);
        await unitOfWork.SaveChangesAsync();

        return approveGradeResult;
    }
}
