using SkillSystem.Application.Services.Grades.Models;

namespace SkillSystem.Application.Services.Roles.Models;

public record RoleResponse : RoleShortInfo
{
    public ICollection<GradeShortInfo> Grades { get; set; }
}
