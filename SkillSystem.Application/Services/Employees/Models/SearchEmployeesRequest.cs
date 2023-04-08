using SkillSystem.Application.Common.Models.Requests;

namespace SkillSystem.Application.Services.Employees.Models;

public record SearchEmployeesRequest : PaginationQuery
{
    public string? Query { get; init; }
}
