namespace SkillSystem.Application.Services.Departments.Models;

public record DepartmentResponse(int Id, string Name, string Description, Guid? HeadEmployeeId);
