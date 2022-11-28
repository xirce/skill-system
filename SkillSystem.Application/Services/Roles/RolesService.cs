using Mapster;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Models.Responses;
using SkillSystem.Application.Repositories.Roles;
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
        var role = await rolesRepository.FindRoleByIdAsync(roleId);

        if (role is null)
            throw new EntityNotFoundException(nameof(Role), roleId);

        return role.Adapt<RoleResponse>();
    }

    public Task<PaginatedResponse<RoleResponse>> FindRolesAsync(SearchRolesRequest request)
    {
        var roles = rolesRepository.FindRoles(request.Title);

        var paginatedRoles = roles
            .ProjectToType<RoleResponse>()
            .ToPaginatedList(request)
            .ToResponse();

        return Task.FromResult(paginatedRoles);
    }

    public async Task UpdateRoleAsync(int roleId, RoleRequest request)
    {
        var role = await rolesRepository.FindRoleByIdAsync(roleId);

        if (role is null)
            throw new EntityNotFoundException(nameof(Role), roleId);

        request.Adapt(role);

        await rolesRepository.UpdateRoleAsync(role);
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        await rolesRepository.DeleteRoleAsync(roleId);
    }
}