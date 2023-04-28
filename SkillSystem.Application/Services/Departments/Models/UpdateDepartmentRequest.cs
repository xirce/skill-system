namespace SkillSystem.Application.Services.Departments.Models;

public record UpdateDepartmentRequest : BaseDepartmentRequest
{
    public int DepartmentId { get; set; }
}
