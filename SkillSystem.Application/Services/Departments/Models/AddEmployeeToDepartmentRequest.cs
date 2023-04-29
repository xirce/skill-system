namespace SkillSystem.Application.Services.Departments.Models;

public record AddEmployeeToDepartmentRequest(Guid EmployeeId, int DepartmentId);
