using SkillSystem.Application.Common.Models;
using SkillSystem.Application.Common.Models.Responses;

namespace SkillSystem.Application.Common.Extensions;

public static class PaginatedListExtensions
{
    public static PaginatedResponse<T> ToResponse<T>(this PaginatedList<T> source)
    {
        return new PaginatedResponse<T>
        {
            Items = source,
            Pagination = new PaginationInfo
            {
                Offset = source.Offset,
                Count = source.Count,
                TotalCount = source.TotalCount
            }
        };
    }
}