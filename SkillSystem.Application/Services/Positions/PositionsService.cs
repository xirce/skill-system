using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Positions;
using SkillSystem.Application.Repositories.Positions.Filters;
using SkillSystem.Application.Services.Positions.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Positions;

public class PositionsService : IPositionsService
{
    private readonly IPositionsRepository positionsRepository;

    public PositionsService(IPositionsRepository positionsRepository)
    {
        this.positionsRepository = positionsRepository;
    }

    public async Task<int> CreatePositionAsync(PositionRequest request)
    {
        var position = request.Adapt<Position>();
        return await positionsRepository.CreatePositionAsync(position);
    }

    public async Task<PositionResponse> GetPositionByIdAsync(int positionId)
    {
        var position = await positionsRepository.GetPositionByIdAsync(positionId);
        return position.Adapt<PositionResponse>();
    }

    public Task<PaginatedResponse<PositionResponse>> FindPositionsAsync(PaginationQuery<PositionFilter> query)
    {
        var positions = positionsRepository.FindPositionsAsync(query.Filter);

        var paginatedPositions = positions
            .ProjectToType<PositionResponse>()
            .ToPaginatedList(query)
            .ToResponse();

        return Task.FromResult(paginatedPositions);
    }

    public async Task UpdatePositionAsync(int positionId, PositionRequest request)
    {
        var position = await positionsRepository.GetPositionByIdAsync(positionId);

        request.Adapt(position);

        await positionsRepository.UpdatePositionAsync(position);
    }

    public async Task DeletePositionAsync(int positionId)
    {
        await positionsRepository.DeletePositionAsync(positionId);
    }
}