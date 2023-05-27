using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class EmployeeGradesRepository : IEmployeeGradesRepository
{
    private readonly SkillSystemDbContext dbContext;

    public EmployeeGradesRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddGradesAsync(IEnumerable<EmployeeGrade> grades)
    {
        foreach (var grade in grades)
        {
            var employeeHasGrade = await HasEmployeeGrade(grade);
            if (!employeeHasGrade)
                await dbContext.EmployeeGrades.AddAsync(grade);
        }
    }

    public async Task<EmployeeGrade?> FindEmployeeGradeAsync(Guid employeeId, int gradeId)
    {
        return await QueryEmployeeGrades(employeeId)
            .FirstOrDefaultAsync(employeeGrade => employeeGrade.GradeId == gradeId);
    }

    public async Task<EmployeeGrade> GetEmployeeGradeAsync(Guid employeeId, int gradeId)
    {
        return await FindEmployeeGradeAsync(employeeId, gradeId)
               ?? throw new EntityNotFoundException(nameof(EmployeeGrade), new { employeeId, gradeId });
    }

    public async Task<IReadOnlyCollection<EmployeeGrade>> FindEmployeeGrades(Guid employeeId, int? roleId = null)
    {
        return await QueryEmployeeGrades(employeeId, roleId).ToListAsync();
    }

    public async Task<EmployeeGrade?> FindLastRoleGradeAsync(Guid employeeId, int roleId)
    {
        var employeeGrades = await QueryEmployeeGrades(employeeId, roleId).ToListAsync();
        var employeeGradeIds = employeeGrades
            .Select(employeeGrade => employeeGrade.GradeId)
            .ToHashSet();
        var lastEmployeeGrade = employeeGrades.FirstOrDefault(
            employeeGrade => !employeeGrade.Grade.NextGradeId.HasValue
                             || !employeeGradeIds.Contains(employeeGrade.Grade.NextGradeId.Value));
        return lastEmployeeGrade;
    }

    public async Task<EmployeeGrade> GetLastRoleGradeAsync(Guid employeeId, int roleId)
    {
        return await FindLastRoleGradeAsync(employeeId, roleId)
               ?? throw new EntityNotFoundException(nameof(EmployeeGrade), new { employeeId, roleId });
    }

    public async Task<IReadOnlyCollection<EmployeeGrade>> FindEmployeeGradesAsync(
        Guid employeeId,
        IEnumerable<int> gradeIds)
    {
        return await QueryEmployeeGrades(employeeId)
            .Where(employeeGrade => gradeIds.Contains(employeeGrade.GradeId))
            .ToListAsync();
    }

    public void UpdateGrades(IEnumerable<EmployeeGrade> grades)
    {
        dbContext.EmployeeGrades.UpdateRange(grades);
    }

    public void DeleteGrades(IEnumerable<EmployeeGrade> grades)
    {
        dbContext.EmployeeGrades.RemoveRange(grades);
    }

    private async Task<bool> HasEmployeeGrade(EmployeeGrade grade)
    {
        return await dbContext.EmployeeGrades
            .AnyAsync(
                employeeGrade =>
                    employeeGrade.EmployeeId == grade.EmployeeId && employeeGrade.GradeId == grade.GradeId);
    }

    private IQueryable<EmployeeGrade> QueryEmployeeGrades(Guid employeeId, int? roleId = null)
    {
        var query = dbContext.EmployeeGrades
            .Include(employeeGrade => employeeGrade.Grade)
            .Where(employeeGrade => employeeGrade.EmployeeId == employeeId);

        if (roleId.HasValue)
            query = query.Where(employeeGrade => employeeGrade.Grade.RoleId == roleId);

        return query;
    }
}
