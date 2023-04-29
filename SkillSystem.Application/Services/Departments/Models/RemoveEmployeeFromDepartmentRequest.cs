namespace SkillSystem.Application.Services.Departments.Models;

public record RemoveEmployeeFromDepartmentRequest(Guid EmployeeId, int DepartmentId);
