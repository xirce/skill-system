namespace SkillSystem.Application.Services.Grading.Models;

public record EmployeeSkillAddedEvent(Guid EmployeeId, IReadOnlyCollection<int> SkillIds);
