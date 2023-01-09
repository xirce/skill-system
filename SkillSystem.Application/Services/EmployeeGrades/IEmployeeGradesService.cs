using SkillSystem.Application.Services.Grades.Models;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.EmployeeGrades;

public interface IEmployeeGradesService
{
    Task AddGradeAsync(string employeeId, int gradeId);
    Task<ICollection<EmployeeGradeResponse>> GetEmployeeGradesAsync(string employeeId);
    Task<ICollection<EmployeeGradeResponse>> FindRoleGradesAsync(string employeeId, int roleId);
    Task<EmployeeGradeResponse> GetRoleLastGradeAsync(string employeeId, int roleId);
    Task SetGradeApprovedAsync(string employeeId, int gradeId, bool isApproved);
}

public record EmployeeGradeResponse
{
    public GradeShortInfo Grade { get; init; }
    public GradeStatus GradeStatus { get; init; }
}