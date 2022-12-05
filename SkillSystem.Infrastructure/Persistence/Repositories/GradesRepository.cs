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
        var grade = await GetGradeByIdAsync(gradeId);
        dbContext.GradeSkills.Remove(new GradeSkill { GradeId = grade.Id, SkillId = skillId });
        await dbContext.SaveChangesAsync();
    }
}