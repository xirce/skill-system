using Mediator;
using SkillSystem.Application.Services.Grading.Grades;
using SkillSystem.Application.Services.Grading.Models;
using SkillSystem.Application.Services.Grading.Skills;

namespace SkillSystem.Application.Services.Grading;

public class EmployeeGradingManager : IEmployeeGradingManager
{
    private readonly IEmployeeGradesService employeeGradesService;
    private readonly IEmployeeSkillsService employeeSkillsService;
    private readonly IPublisher publisher;

    public EmployeeGradingManager(
        IEmployeeGradesService employeeGradesService,
        IEmployeeSkillsService employeeSkillsService,
        IPublisher publisher)
    {
        this.employeeGradesService = employeeGradesService;
        this.employeeSkillsService = employeeSkillsService;
        this.publisher = publisher;
    }

    public async Task GradeEmployee(GradeEmployeeRequest request)
    {
        var gradeEmployeeResult =
            await employeeGradesService.AddEmployeeGradeAsync(request.EmployeeId, request.GradeId);
        await publisher.Publish(new EmployeeGradedEvent(request.EmployeeId, gradeEmployeeResult.AffectedGradeIds));
    }

    public async Task ApproveEmployeeGrade(ApproveGradeRequest request)
    {
        var approveGradeResult =
            await employeeGradesService.ApproveEmployeeGradeAsync(request.EmployeeId, request.GradeId);
        await publisher.Publish(
            new EmployeeGradeApprovedEvent(request.EmployeeId, approveGradeResult.AffectedGradeIds));
    }

    public async Task AddSkillToEmployee(AddEmployeeSkillRequest request)
    {
        var addSkillResult =
            await employeeSkillsService.AddEmployeeSkillsAsync(request.EmployeeId, new[] { request.SkillId });
        await publisher.Publish(new EmployeeSkillAddedEvent(request.EmployeeId, addSkillResult.AffectedSkillIds));
    }

    public async Task ApproveEmployeeSkill(ApproveEmployeeSkillRequest request)
    {
        var approveSkillResult =
            await employeeSkillsService.ApproveSkillsAsync(request.EmployeeId, new[] { request.SkillId });
        await publisher.Publish(
            new EmployeeSkillApprovedEvent(request.EmployeeId, approveSkillResult.AffectedSkillIds));
    }

    public async Task DeleteEmployeeSkill(DeleteEmployeeSkillRequest request)
    {
        var deleteSkillResult = await employeeSkillsService.DeleteEmployeeSkillsAsync(
            request.EmployeeId, new[] { request.SkillId });
        await publisher.Publish(new EmployeeSkillDeletedEvent(request.EmployeeId, deleteSkillResult.AffectedSkillIds));
    }
}
