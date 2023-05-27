namespace SkillSystem.Application.Services.Grading.Grades;

public record EmployeeGradeChangeResult(Guid EmployeeId, IReadOnlyCollection<int> AffectedGradeIds);
