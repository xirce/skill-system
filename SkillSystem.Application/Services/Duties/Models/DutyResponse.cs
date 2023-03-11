namespace SkillSystem.Application.Services.Duties.Models;

public record DutyResponse : DutyShortInfo
{
    public string Description { get; init; }
}
