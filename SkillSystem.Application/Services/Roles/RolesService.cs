using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Application.Services.Grades.Models;
using SkillSystem.Application.Services.Roles.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Roles;

public class RolesService : IRolesService
{
    private readonly IRolesRepository rolesRepository;

    public RolesService(IRolesRepository rolesRepository)
    {
        this.rolesRepository = rolesRepository;
    }

    public async Task<int> CreateRoleAsync(RoleRequest request)
    {
        var role = request.Adapt<Role>();
        return await rolesRepository.CreateRoleAsync(role);
    }

    public async Task<RoleResponse> GetRoleByIdAsync(int roleId)
    {
        var role = await rolesRepository.GetRoleByIdAsync(roleId);

        var roleResponse = role.Adapt<RoleResponse>();
        var sortedGrades = role.Grades.Sort();
        roleResponse.Grades = sortedGrades.Adapt<ICollection<GradeShortInfo>>();

        return roleResponse;
    }

    public Task<PaginatedResponse<RoleShortInfo>> FindRolesAsync(SearchRolesRequest request)
    {
        var roles = rolesRepository.FindRoles(request.Title);

        var paginatedRoles = roles
            .ProjectToType<RoleShortInfo>()
            .ToPaginatedList(request)
            .ToResponse();

        return Task.FromResult(paginatedRoles);
    }

    public async Task<ICollection<GradeShortInfo>> GetRoleGradesAsync(int roleId)
    {
        var grades = await rolesRepository.GetRoleGradesAsync(roleId);
        var sortedGrades = grades.Sort();
        return sortedGrades.Adapt<ICollection<GradeShortInfo>>();
    }

    public async Task<ICollection<GradeWithSkills>> GetRoleGradesWithSkillsAsync(int roleId)
    {
        var grades = await rolesRepository.GetRoleGradesAsync(roleId, true);
        var sortedGrades = grades.Sort();
        return sortedGrades.Adapt<ICollection<GradeWithSkills>>();
    }

    public async Task<int> AddGradeAsync(int roleId, AddGradeRequest request)
    {
        var grade = request.Adapt<Grade>();
        return await rolesRepository.AddGradeAfterAsync(roleId, grade, request.PrevGradeId);
    }

    public async Task InsertGradeAfterAsync(int roleId, int gradeId, int? prevGradeId)
    {
        await rolesRepository.InsertGradeAfterAsync(roleId, gradeId, prevGradeId);
    }

    public async Task UpdateRoleAsync(int roleId, RoleRequest request)
    {
        var role = await rolesRepository.GetRoleByIdAsync(roleId);
        request.Adapt(role);
        await rolesRepository.UpdateRoleAsync(role);
    }

    public async Task DeleteGradeAsync(int roleId, int gradeId)
    {
        await rolesRepository.DeleteGradeAsync(roleId, gradeId);
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        var role = await rolesRepository.GetRoleByIdAsync(roleId);
        await rolesRepository.DeleteRoleAsync(role);
    }
}
