using Microsoft.Extensions.DependencyInjection;
using SkillSystem.Application.Services.Skills;

namespace SkillSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISkillsService, SkillsService>();

        return services;
    }
}