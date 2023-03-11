namespace SkillSystem.Application.Common.Models.Responses;

public class PaginationInfo
{
    public int Offset { get; set; }
    public int Count { get; set; }
    public int TotalCount { get; set; }
}