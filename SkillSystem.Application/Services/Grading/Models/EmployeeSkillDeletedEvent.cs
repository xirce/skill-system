namespace SkillSystem.Application.Services.Grading.Models;

public record EmployeeSkillDeletedEvent(Guid EmployeeId, IReadOnlyCollection<int> SkillIds);
