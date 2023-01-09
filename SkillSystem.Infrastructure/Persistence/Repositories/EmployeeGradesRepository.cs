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

    public async Task AddGradesAsync(string employeeId, IEnumerable<Grade> grades)
    {
        foreach (var grade in grades)
        {
            var presentGrade = await FindEmployeeGradeAsync(employeeId, grade.Id);
            if (presentGrade is null)
                await dbContext.EmployeeGrades.AddAsync(
                    new EmployeeGrade { EmployeeId = employeeId, GradeId = grade.Id }
                );
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<EmployeeGrade?> FindEmployeeGradeAsync(string employeeId, int gradeId)
    {
        return await dbContext.EmployeeGrades
            .Include(employeeGrade => employeeGrade.Grade)
            .FirstOrDefaultAsync(
                employeeGrade => employeeGrade.EmployeeId == employeeId && employeeGrade.GradeId == gradeId
            );
    }

    public async Task<EmployeeGrade> GetEmployeeGradeAsync(string employeeId, int gradeId)
    {
        return await FindEmployeeGradeAsync(employeeId, gradeId)
               ?? throw new EntityNotFoundException(nameof(EmployeeGrade), new { employeeId, gradeId });
    }

    public async Task<ICollection<EmployeeGrade>> GetEmployeeGradesAsync(string employeeId)
    {
        return await FindEmployeeGrades(employeeId).ToListAsync();
    }

    public async Task<ICollection<EmployeeGrade>> FindEmployeeGradesAsync(string employeeId, IEnumerable<int> gradesIds)
    {
        return await FindEmployeeGrades(employeeId)
            .Where(employeeGrade => gradesIds.Contains(employeeGrade.GradeId))
            .ToListAsync();
    }

    public async Task UpdateGrade(EmployeeGrade employeeGrade)
    {
        dbContext.EmployeeGrades.Update(employeeGrade);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteGrade(EmployeeGrade employeeGrade)
    {
        dbContext.EmployeeGrades.Remove(employeeGrade);
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<EmployeeGrade> FindEmployeeGrades(string employeeId)
    {
        return dbContext.EmployeeGrades.Where(employeeGrade => employeeGrade.EmployeeId == employeeId);
    }
}