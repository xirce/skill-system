using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Positions.Filters;
using SkillSystem.Application.Services.Duties.Models;
using SkillSystem.Application.Services.Positions.Models;

namespace SkillSystem.Application.Services.Positions;

public interface IPositionsService
{
    Task<int> CreatePositionAsync(PositionRequest request);
    Task<PositionResponse> GetPositionByIdAsync(int positionId);
    Task<PaginatedResponse<PositionResponse>> FindPositionsAsync(PaginationQuery<PositionFilter> query);
    Task<ICollection<DutyShortInfo>> GetPositionDutiesAsync(int positionId);
    Task UpdatePositionAsync(int positionId, PositionRequest request);
    Task AddPositionDutyAsync(int positionId, int dutyId);
    Task DeletePositionDutyAsync(int positionId, int dutyId);
    Task DeletePositionAsync(int positionId);
}