namespace SkillSystem.Application.Services.Grading.Models;

public record EmployeeGradeApprovedEvent(Guid EmployeeId, IReadOnlyCollection<int> GradeIds);
