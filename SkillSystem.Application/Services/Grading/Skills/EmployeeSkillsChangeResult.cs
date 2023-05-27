namespace SkillSystem.Application.Services.Grading.Skills;

public record EmployeeSkillsChangeResult(Guid EmployeeId, IReadOnlyCollection<int> AffectedSkillIds);
