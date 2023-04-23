using SkillSystem.Application.Common.Models;
using SkillSystem.Application.Common.Models.Requests;

namespace SkillSystem.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, PaginationQuery paginationQuery)
    {
        return source.ToPaginatedList(paginationQuery.Offset, paginationQuery.Count);
    }

    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, int offset, int count)
    {
        var totalCount = source.Count();
        var paginatedItems = source
            .Skip(offset)
            .Take(count)
            .ToList();

        return PaginatedList.Create(paginatedItems, offset, totalCount);
    }
}
