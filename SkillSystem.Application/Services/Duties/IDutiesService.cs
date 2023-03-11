using SkillSystem.Application.Common.Models.Requests;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Duties.Filters;
using SkillSystem.Application.Services.Duties.Models;

namespace SkillSystem.Application.Services.Duties;

public interface IDutiesService
{
    Task<int> CreateDutyAsync(DutyRequest request);
    Task<DutyResponse> GetDutyByIdAsync(int dutyId);
    Task<PaginatedResponse<DutyShortInfo>> FindDutiesAsync(PaginationQuery<DutyFilter> query);
    Task UpdateDutyAsync(int dutyId, DutyRequest request);
    Task DeleteDutyAsync(int dutyId);
}
