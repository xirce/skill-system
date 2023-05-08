using SkillSystem.Application.Services.Grading.Models;

namespace SkillSystem.Application.Services.Grading;

public interface IEmployeeGradingManager
{
    Task GradeEmployee(GradeEmployeeRequest request);
    Task ApproveEmployeeGrade(ApproveGradeRequest request);
    Task AddSkillToEmployee(AddEmployeeSkillRequest request);
    Task ApproveEmployeeSkill(ApproveEmployeeSkillRequest request);
    Task DeleteEmployeeSkill(DeleteEmployeeSkillRequest request);
}
