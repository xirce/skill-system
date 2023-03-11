using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class GradesRepository : IGradesRepository
{
    private readonly SkillSystemDbContext dbContext;

    public GradesRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Grade?> FindGradeByIdAsync(int gradeId)
    {
        return await dbContext.Grades
            .Include(grade => grade.PrevGrade)
            .Include(grade => grade.NextGrade)
            .Include(grade => grade.Skills.OrderBy(skill => skill.Id))
            .ThenInclude(grade => grade.SubSkills.OrderBy(skill => skill.Id))
            .FirstOrDefaultAsync(grade => grade.Id == gradeId);
    }

    public async Task<Grade> GetGradeByIdAsync(int gradeId)
    {
        return await FindGradeByIdAsync(gradeId) ?? throw new EntityNotFoundException(nameof(Grade), gradeId);
    }

    public IQueryable<Grade> FindGrades(string? title = default)
    {
        var query = dbContext.Grades.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(grade => grade.Title.Contains(title));

        return query.OrderBy(grade => grade.Id);
    }

    public async Task<IEnumerable<Skill>> GetGradeSkillsAsync(int gradeId)
    {
        var gradeSkills = await dbContext.Grades
            .AsNoTracking()
            .Include(grade => grade.Skills.OrderBy(skill => skill.Id))
            .ThenInclude(skill => skill.SubSkills.OrderBy(subSkill => subSkill.Id))
            .Where(grade => grade.Id == gradeId)
            .Select(grade => grade.Skills)
            .FirstOrDefaultAsync();

        if (gradeSkills is null)
            throw new EntityNotFoundException(nameof(Grade), gradeId);

        return gradeSkills;
    }

    public async Task<ICollection<Position>> GetGradePositionsAsync(int gradeId)
    {
        var gradePositions = await dbContext.Grades
            .AsNoTracking()
            .Include(grade => grade.Positions.OrderBy(position => position.Id))
            .Where(grade => grade.Id == gradeId)
            .Select(grade => grade.Positions)
            .FirstOrDefaultAsync();

        if (gradePositions is null)
            throw new EntityNotFoundException(nameof(Grade), gradeId);

        return gradePositions;
    }

    public async Task UpdateGradeAsync(Grade grade)
    {
        dbContext.Grades.Update(grade);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddGradeSkillAsync(int gradeId, Skill skill)
    {
        var grade = await GetGradeByIdAsync(gradeId);

        var gradeSkill = new GradeSkill
        {
            GradeId = grade.Id,
            SkillId = skill.Id
        };

        await dbContext.GradeSkills.AddAsync(gradeSkill);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteGradeSkillAsync(int gradeId, int skillId)
    {
        var gradeSkill = await GetGradeSkillAsync(gradeId, skillId);
        dbContext.GradeSkills.Remove(gradeSkill);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddGradePositionAsync(int gradeId, Position position)
    {
        var grade = await GetGradeByIdAsync(gradeId);
        var currentPositionGrade = await FindCurrentPositionGrade(grade.RoleId, position.Id);

        if (currentPositionGrade is not null)
        {
            dbContext.PositionGrades.Remove(currentPositionGrade);
        }

        var newPositionGrade = new PositionGrade
        {
            GradeId = grade.Id,
            PositionId = position.Id
        };
        await dbContext.PositionGrades.AddAsync(newPositionGrade);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteGradePositionAsync(int gradeId, int positionId)
    {
        var positionGrade = await GetGradePositionAsync(gradeId, positionId);
        dbContext.PositionGrades.Remove(positionGrade);
        await dbContext.SaveChangesAsync();
    }

    private async Task<GradeSkill> GetGradeSkillAsync(int gradeId, int skillId)
    {
        var positionGrade = await dbContext.GradeSkills
            .AsNoTracking()
            .FirstOrDefaultAsync(positionGrade => positionGrade.GradeId == gradeId && positionGrade.SkillId == skillId);

        if (positionGrade is null)
            throw new EntityNotFoundException(nameof(GradeSkill), new { gradeId, skillId });

        return positionGrade;
    }

    private async Task<PositionGrade?> FindCurrentPositionGrade(int roleId, int positionId)
    {
        return await dbContext.PositionGrades
            .FirstOrDefaultAsync(
                positionGrade => positionGrade.Grade.RoleId == roleId && positionGrade.PositionId == positionId);
    }

    private async Task<PositionGrade> GetGradePositionAsync(int gradeId, int positionId)
    {
        var positionGrade = await dbContext.PositionGrades
            .AsNoTracking()
            .FirstOrDefaultAsync(
                positionGrade => positionGrade.GradeId == gradeId && positionGrade.PositionId == positionId);

        if (positionGrade is null)
            throw new EntityNotFoundException(nameof(PositionGrade), new { positionId, gradeId });

        return positionGrade;
    }
}
