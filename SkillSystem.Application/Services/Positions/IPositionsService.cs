using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Positions.Filters;
using SkillSystem.Application.Services.Positions.Models;

namespace SkillSystem.Application.Services.Positions;

public interface IPositionsService
{
    Task<int> CreatePositionAsync(PositionRequest request);
    Task<PositionResponse> GetPositionByIdAsync(int positionId);
    Task<PaginatedResponse<PositionResponse>> FindPositionsAsync(PaginationQuery<PositionFilter> query);
    Task UpdatePositionAsync(int positionId, PositionRequest request);
    Task DeletePositionAsync(int positionId);
}