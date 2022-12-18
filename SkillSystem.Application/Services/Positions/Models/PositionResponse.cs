namespace SkillSystem.Application.Services.Positions.Models;

public record PositionResponse
{
    public int Id { get; init; }
    public string Title { get; init; }
}