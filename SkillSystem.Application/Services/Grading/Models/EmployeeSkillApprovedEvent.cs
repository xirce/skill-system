namespace SkillSystem.Application.Services.Grading.Models;

public record EmployeeSkillApprovedEvent(Guid EmployeeId, IReadOnlyCollection<int> SkillIds);
