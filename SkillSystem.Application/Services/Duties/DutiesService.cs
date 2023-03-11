using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Duties;
using SkillSystem.Application.Repositories.Duties.Filters;
using SkillSystem.Application.Services.Duties.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Duties;

public class DutiesService : IDutiesService
{
    private readonly IDutiesRepository dutiesRepository;

    public DutiesService(IDutiesRepository dutiesRepository)
    {
        this.dutiesRepository = dutiesRepository;
    }

    public async Task<int> CreateDutyAsync(DutyRequest request)
    {
        var duty = request.Adapt<Duty>();
        return await dutiesRepository.CreateDutyAsync(duty);
    }

    public async Task<DutyResponse> GetDutyByIdAsync(int dutyId)
    {
        var duty = await dutiesRepository.GetDutyByIdAsync(dutyId);
        return duty.Adapt<DutyResponse>();
    }

    public Task<PaginatedResponse<DutyShortInfo>> FindDutiesAsync(PaginationQuery<DutyFilter> query)
    {
        var duties = dutiesRepository.FindDuties(query.Filter);

        var paginatedDuties = duties
            .ProjectToType<DutyShortInfo>()
            .ToPaginatedList(query)
            .ToResponse();

        return Task.FromResult(paginatedDuties);
    }

    public async Task UpdateDutyAsync(int dutyId, DutyRequest request)
    {
        var duty = await dutiesRepository.GetDutyByIdAsync(dutyId);

        request.Adapt(duty);

        await dutiesRepository.UpdateDutyAsync(duty);
    }

    public async Task DeleteDutyAsync(int dutyId)
    {
        var duty = await dutiesRepository.GetDutyByIdAsync(dutyId);
        await dutiesRepository.DeleteDutyAsync(duty);
    }
}
