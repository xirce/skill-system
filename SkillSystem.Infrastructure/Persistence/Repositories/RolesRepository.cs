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

    public async Task<ICollection<Grade>> GetRoleGradesAsync(int roleId, bool includeSkills = false)
    {
        await EnsureRoleExists(roleId);

        var roleGrades = dbContext.Roles
            .Include(role => role.Grades.OrderBy(grade => grade.Id))
            .ThenInclude(grade => grade.PrevGrade)
            .Where(role => role.Id == roleId)
            .SelectMany(role => role.Grades);

        if (includeSkills)
            roleGrades = roleGrades.Include(grade => grade.Skills.OrderBy(skill => skill.Id));

        return await roleGrades.ToListAsync();
    }

    public async Task UpdateRoleAsync(Role role)
    {
        dbContext.Roles.Update(role);
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> AddGradeAfterAsync(int roleId, Grade grade, int? prevGradeId)
    {
        var prevGrade = prevGradeId.HasValue ? await GetRoleGradeAsync(roleId, prevGradeId.Value) : null;

        grade.RoleId = roleId;
        grade.PrevGradeId = null;
        await dbContext.Grades.AddAsync(grade);
        await dbContext.SaveChangesAsync();

        await InsertGradeAfterAsync(grade, prevGrade);
        await dbContext.SaveChangesAsync();

        return grade.Id;
    }

    public async Task InsertGradeAfterAsync(int roleId, int gradeId, int? prevGradeId)
    {
        var grade = await GetRoleGradeAsync(roleId, gradeId);
        var prevGrade = prevGradeId.HasValue ? await GetRoleGradeAsync(roleId, prevGradeId.Value) : null;

        await DetachGradeAsync(grade);
        await InsertGradeAfterAsync(grade, prevGrade);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteGradeAsync(int roleId, int gradeId)
    {
        var grade = await GetRoleGradeAsync(roleId, gradeId);

        await DetachGradeAsync(grade, toDelete: true);
    }

    public async Task DeleteRoleAsync(Role role)
    {
        dbContext.Roles.Remove(role);
        await dbContext.SaveChangesAsync();
    }

    private async Task<Grade> GetRoleGradeAsync(int roleId, int gradeId)
    {
        var grade = await FindRoleGradeAsync(roleId, grade => grade.Id == gradeId);
        if (grade is null)
            throw new EntityNotFoundException("RoleGrade", new { roleId, gradeId });
        return grade;
    }

    private async Task EnsureRoleExists(int roleId)
    {
        var roleExists = await dbContext.Roles.AnyAsync(role => role.Id == roleId);
        if (!roleExists)
            throw new EntityNotFoundException(nameof(Role), roleId);
    }

    private async Task InsertGradeAfterAsync(Grade insertGrade, Grade? prevGrade)
    {
        insertGrade.PrevGradeId = null;
        if (prevGrade is null)
        {
            var firstGrade = await FindRoleFirstGradeAsync(insertGrade.RoleId);
            if (firstGrade is not null && firstGrade.Id != insertGrade.Id)
                firstGrade.PrevGradeId = insertGrade.Id;
            return;
        }

        insertGrade.PrevGradeId = prevGrade.Id;

        var nextGrade = prevGrade.NextGrade;
        if (nextGrade is not null)
            nextGrade.PrevGradeId = insertGrade.Id;
    }

    private async Task<Grade?> FindRoleFirstGradeAsync(int roleId)
    {
        return await FindRoleGradeAsync(roleId, grade => grade.PrevGradeId == null);
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

    private async Task DetachGradeAsync(Grade grade, bool toDelete = false)
    {
        var prevGrade = grade.PrevGrade;
        var nextGrade = grade.NextGrade;

        if (toDelete)
        {
            dbContext.Grades.Remove(grade);
            await dbContext.SaveChangesAsync();
        }

        if (nextGrade is not null && prevGrade is not null)
        {
            nextGrade.PrevGradeId = prevGrade.Id;
            nextGrade.PrevGrade = prevGrade;
        }

        grade.PrevGradeId = null;
        grade.PrevGrade = null;
        grade.NextGrade = null;
        await dbContext.SaveChangesAsync();
    }
}
