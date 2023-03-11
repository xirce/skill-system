using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Common.Models.Requests;

public record PaginationQuery<T> : PaginationQuery
{
    public T? Filter { get; init; }
}

public record PaginationQuery
{
    [Range(0, int.MaxValue)]
    public int Offset { get; init; } = 0;

    [Range(0, 150)]
    public int Count { get; init; } = 100;
}
