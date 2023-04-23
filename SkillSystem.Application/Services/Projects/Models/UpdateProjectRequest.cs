namespace SkillSystem.Application.Services.Projects.Models;

public record UpdateProjectRequest : BaseProjectRequest
{
    public int ProjectId { get; init; }
}
