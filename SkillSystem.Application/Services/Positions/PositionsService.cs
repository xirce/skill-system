using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories;
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
    private readonly IUnitOfWork unitOfWork;

    public PositionsService(
        IPositionsRepository positionsRepository,
        IDutiesRepository dutiesRepository,
        IUnitOfWork unitOfWork)
    {
        this.positionsRepository = positionsRepository;
        this.dutiesRepository = dutiesRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<int> CreatePositionAsync(PositionRequest request)
    {
        var position = request.Adapt<Position>();
        await positionsRepository.CreatePositionAsync(position);
        await unitOfWork.SaveChangesAsync();
        return position.Id;
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

        positionsRepository.UpdatePosition(position);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task AddPositionDutyAsync(int positionId, int dutyId)
    {
        var duty = await dutiesRepository.GetDutyByIdAsync(dutyId);
        await positionsRepository.AddPositionDutyAsync(positionId, duty);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePositionDutyAsync(int positionId, int dutyId)
    {
        await positionsRepository.DeletePositionDutyAsync(positionId, dutyId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePositionAsync(int positionId)
    {
        var position = await positionsRepository.GetPositionByIdAsync(positionId);
        positionsRepository.DeletePosition(position);
        await unitOfWork.SaveChangesAsync();
    }
}
