namespace SkillSystem.Application.Common.Models;

public class PaginatedList<T> : List<T>
{
    public int Offset { get; }
    public int TotalCount { get; }

    public PaginatedList(IReadOnlyCollection<T> items, int offset, int totalCount) : base(items)
    {
        if (totalCount < items.Count)
            throw new ArgumentException("Total count cannot be less than items count", nameof(totalCount));

        Offset = offset;
        TotalCount = totalCount;
    }
}

public static class PaginatedList
{
    public static PaginatedList<T> Create<T>(IReadOnlyCollection<T> items, int offset, int totalCount)
    {
        return new PaginatedList<T>(items, offset, totalCount);
    }
}
