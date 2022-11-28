namespace SkillSystem.Application.Common.Models.Responses;

public class PaginatedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public PaginationResponse Pagination { get; set; }
}