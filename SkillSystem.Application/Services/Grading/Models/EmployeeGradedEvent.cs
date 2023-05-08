namespace SkillSystem.Application.Services.Grading.Models;

public record EmployeeGradedEvent(Guid EmployeeId, IReadOnlyCollection<int> GradeIds);
