namespace SkillSystem.Application.Services.Projects.Models;

public record AddRoleToProjectRequest(int ProjectId, int RoleId, Guid? EmployeeId);
