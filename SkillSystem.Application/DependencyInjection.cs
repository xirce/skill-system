using Microsoft.Extensions.DependencyInjection;
using SkillSystem.Application.Services.Grades;
using SkillSystem.Application.Services.Roles;
using SkillSystem.Application.Services.Skills;

namespace SkillSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISkillsService, SkillsService>();
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<IGradesService, GradesService>();

        return services;
    }
}