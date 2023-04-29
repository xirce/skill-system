using SkillSystem.Application.Repositories.Positions.Filters;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Positions;

public interface IPositionsRepository
{
    Task CreatePositionAsync(Position position);
    Task<Position?> FindPositionByIdAsync(int positionId);
    Task<Position> GetPositionByIdAsync(int positionId);
    IQueryable<Position> FindPositionsAsync(PositionFilter? filter = default);
    Task<ICollection<Duty>> GetPositionDutiesAsync(int positionId);
    void UpdatePosition(Position position);
    Task AddPositionDutyAsync(int positionId, Duty duty);
    Task DeletePositionDutyAsync(int positionId, int dutyId);
    void DeletePosition(Position position);
}
