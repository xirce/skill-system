using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly SkillSystemDbContext dbContext;

    public RolesRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreateRoleAsync(Role role)
    {
        await dbContext.Roles.AddAsync(role);
        await dbContext.SaveChangesAsync();
        return role.Id;
    }

    public async Task<Role?> FindRoleByIdAsync(int roleId)
    {
        return await dbContext.Roles
            .Include(role => role.Grades.OrderBy(grade => grade.Id))
            .ThenInclude(grade => grade.PrevGrade)
            .FirstOrDefaultAsync(role => role.Id == roleId);
    }

    public async Task<Role> GetRoleByIdAsync(int roleId)
    {
        return await FindRoleByIdAsync(roleId) ?? throw new EntityNotFoundException(nameof(Role), roleId);
    }

    public IQueryable<Role> FindRoles(string? title = default)
    {
        var roles = dbContext.Roles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(title))
            roles = roles.Where(role => role.Title.Contains(title));

        return roles.OrderBy(role => role.Id);
    }

    public async Task<ICollection<Grade>> GetRoleGradesAsync(int roleId)
    {
        var roleGrades = await dbContext.Roles
            .Include(role => role.Grades.OrderBy(grade => grade.Id))
            .ThenInclude(grade => grade.PrevGrade)
            .Where(role => role.Id == roleId)
            .Select(role => role.Grades)
            .FirstOrDefaultAsync();

        if (roleGrades is null)
            throw new EntityNotFoundException(nameof(Role), roleId);

        return roleGrades;
    }

    public async Task UpdateRoleAsync(Role role)
    {
        dbContext.Roles.Update(role);
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> AddGradeAfterAsync(int roleId, Grade grade, int? prevGradeId)
    {
        await EnsureRoleExists(roleId);

        grade.RoleId = roleId;
        await dbContext.Grades.AddAsync(grade);
        await dbContext.SaveChangesAsync();

        await InsertGradeAfterAsync(roleId, grade, prevGradeId);
        await dbContext.SaveChangesAsync();

        return grade.Id;
    }

    public async Task InsertGradeAfterAsync(int roleId, int gradeId, int? prevGradeId)
    {
        var grade = await GetRoleGradeAsync(roleId, gradeId);

        DetachGrade(grade);
        await InsertGradeAfterAsync(roleId, grade, prevGradeId);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteGradeAsync(int roleId, int gradeId)
    {
        var grade = await GetRoleGradeAsync(roleId, gradeId);

        DetachGrade(grade);
        dbContext.Grades.Remove(grade);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        dbContext.Roles.Remove(new Role { Id = roleId });
        await dbContext.SaveChangesAsync();
    }

    private async Task<Grade> GetRoleGradeAsync(int roleId, int gradeId)
    {
        var grade = await FindRoleGradeAsync(roleId, grade => grade.Id == gradeId);
        if (grade is null || grade.RoleId != roleId)
            throw new EntityNotFoundException("RoleGrade", new { roleId, gradeId });
        return grade;
    }

    private async Task<Grade?> FindRoleGradeAsync(int roleId, Expression<Func<Grade, bool>> predicate)
    {
        await EnsureRoleExists(roleId);

        var grade = await dbContext.Grades
            .Include(grade => grade.PrevGrade)
            .Include(grade => grade.NextGrade)
            .Where(grade => grade.RoleId == roleId)
            .FirstOrDefaultAsync(predicate);

        return grade;
    }

    private async Task EnsureRoleExists(int roleId)
    {
        var roleExists = await dbContext.Roles.AnyAsync(role => role.Id == roleId);
        if (!roleExists)
            throw new EntityNotFoundException(nameof(Role), roleId);
    }

    private async Task InsertGradeAfterAsync(int roleId, Grade insertGrade, int? prevGradeId)
    {
        if (!prevGradeId.HasValue)
        {
            insertGrade.PrevGradeId = null;
            var firstGrade = await FindRoleGradeAsync(roleId, grade => grade.PrevGradeId == null);
            if (firstGrade is not null && firstGrade.Id != insertGrade.Id)
                firstGrade.PrevGradeId = insertGrade.Id;
            return;
        }

        var prevGrade = await GetRoleGradeAsync(roleId, prevGradeId.Value);
        insertGrade.PrevGradeId = prevGrade.Id;

        var nextGrade = prevGrade.NextGrade;
        if (nextGrade is not null)
            nextGrade.PrevGradeId = insertGrade.Id;
    }

    private static void DetachGrade(Grade grade)
    {
        var nextGrade = grade.NextGrade;
        if (nextGrade is not null)
        {
            nextGrade.PrevGradeId = grade.PrevGradeId;
            nextGrade.PrevGrade = grade.PrevGrade;
        }

        var prevGrade = grade.PrevGrade;
        if (prevGrade is not null)
            prevGrade.NextGrade = nextGrade;
    }
}