using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Duties;
using SkillSystem.Application.Repositories.Positions;
using SkillSystem.Application.Repositories.Positions.Filters;
using SkillSystem.Application.Services.Duties.Models;
using SkillSystem.Application.Services.Positions.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Positions;

public class PositionsService : IPositionsService
{
    private readonly IPositionsRepository positionsRepository;
    private readonly IDutiesRepository dutiesRepository;

    public PositionsService(IPositionsRepository positionsRepository, IDutiesRepository dutiesRepository)
    {
        this.positionsRepository = positionsRepository;
        this.dutiesRepository = dutiesRepository;
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

    public async Task<ICollection<DutyShortInfo>> GetPositionDutiesAsync(int positionId)
    {
        var duties = await positionsRepository.GetPositionDutiesAsync(positionId);
        return duties.Adapt<ICollection<DutyShortInfo>>();
    }

    public async Task UpdatePositionAsync(int positionId, PositionRequest request)
    {
        var position = await positionsRepository.GetPositionByIdAsync(positionId);

        request.Adapt(position);

        await positionsRepository.UpdatePositionAsync(position);
    }

    public async Task AddPositionDutyAsync(int positionId, int dutyId)
    {
        var duty = await dutiesRepository.GetDutyByIdAsync(dutyId);
        await positionsRepository.AddPositionDutyAsync(positionId, duty);
    }

    public async Task DeletePositionDutyAsync(int positionId, int dutyId)
    {
        await positionsRepository.DeletePositionDutyAsync(positionId, dutyId);
    }

    public async Task DeletePositionAsync(int positionId)
    {
        await positionsRepository.DeletePositionAsync(positionId);
    }
}