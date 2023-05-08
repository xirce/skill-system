using SkillSystem.Application.Services.Grades.Models;
using SkillSystem.Core.Enums;

namespace SkillSystem.Application.Services.Grading.Grades.Models;

public record EmployeeGradeResponse
{
    public GradeShortInfo Grade { get; init; }
    public EmployeeGradeStatus Status { get; init; }
}
