namespace SkillSystem.IdentityServer4.Models.Common;

public class PaginationResponse
{
    public int Offset { get; set; }
    public int Count { get; set; }
    public int TotalCount { get; set; }
}