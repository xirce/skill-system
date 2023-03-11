namespace SkillSystem.IdentityServer4.Models.Common;

public class PaginatedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public PaginationResponse Pagination { get; set; }
}
