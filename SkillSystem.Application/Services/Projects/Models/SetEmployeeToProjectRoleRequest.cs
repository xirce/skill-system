namespace SkillSystem.Application.Services.Projects.Models;

public record SetEmployeeToProjectRoleRequest(int ProjectRoleId, Guid? EmployeeId);
