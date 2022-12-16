using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Infrastructure.Persistence;
using SkillSystem.Infrastructure.Persistence.Repositories;

namespace SkillSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<SkillSystemDbContext>(options => options.UseInMemoryDatabase("SkillSystem"));

        services.AddScoped<ISkillsRepository, SkillsRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IGradesRepository, GradesRepository>();

        return services;
    }
}