using Microsoft.Extensions.DependencyInjection;
using SkillSystem.Application.Services.Departments;
using SkillSystem.Application.Services.Duties;
using SkillSystem.Application.Services.Employees;
using SkillSystem.Application.Services.Employees.Manager;
using SkillSystem.Application.Services.EmployeeSkills;
using SkillSystem.Application.Services.Grades;
using SkillSystem.Application.Services.Positions;
using SkillSystem.Application.Services.Projects;
using SkillSystem.Application.Services.Roles;
using SkillSystem.Application.Services.Salaries;
using SkillSystem.Application.Services.Skills;

namespace SkillSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISkillsService, SkillsService>();
        services.AddScoped<IRolesService, RolesService>();
        services.AddScoped<IGradesService, GradesService>();
        services.AddScoped<IPositionsService, PositionsService>();
        services.AddScoped<IDutiesService, DutiesService>();
        services.AddScoped<ISalariesService, SalariesService>();

        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<IProjectRolesService, ProjectRolesService>();
        services.AddScoped<IDepartmentsService, DepartmentsService>();

        services.AddScoped<IEmployeeSkillsService, EmployeeSkillsService>();

        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IEmployeesManager, EmployeesManager>();

        return services;
    }
}
