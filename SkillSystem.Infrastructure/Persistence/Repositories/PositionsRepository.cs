using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Positions;
using SkillSystem.Application.Repositories.Positions.Filters;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class PositionsRepository : IPositionsRepository
{
    private readonly SkillSystemDbContext dbContext;

    public PositionsRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> CreatePositionAsync(Position position)
    {
        await dbContext.Positions.AddAsync(position);
        await dbContext.SaveChangesAsync();
        return position.Id;
    }

    public async Task<Position?> FindPositionByIdAsync(int positionId)
    {
        return await dbContext.Positions.FirstOrDefaultAsync(position => position.Id == positionId);
    }

    public async Task<Position> GetPositionByIdAsync(int positionId)
    {
        return await FindPositionByIdAsync(positionId)
               ?? throw new EntityNotFoundException(nameof(Position), positionId);
    }

    public IQueryable<Position> FindPositionsAsync(PositionFilter? filter = default)
    {
        var positions = dbContext.Positions.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Title))
            positions = positions.Where(role => role.Title.Contains(filter.Title));

        return positions.OrderBy(role => role.Id);
    }

    public async Task UpdatePositionAsync(Position position)
    {
        dbContext.Positions.Update(position);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeletePositionAsync(int positionId)
    {
        dbContext.Positions.Remove(new Position { Id = positionId });
        await dbContext.SaveChangesAsync();
    }
}