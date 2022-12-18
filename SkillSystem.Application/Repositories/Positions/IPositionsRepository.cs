using SkillSystem.Application.Repositories.Positions.Filters;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Positions;

public interface IPositionsRepository
{
    Task<int> CreatePositionAsync(Position position);
    Task<Position?> FindPositionByIdAsync(int positionId);
    Task<Position> GetPositionByIdAsync(int positionId);
    IQueryable<Position> FindPositionsAsync(PositionFilter? filter = default);
    Task UpdatePositionAsync(Position position);
    Task DeletePositionAsync(int positionId);
}