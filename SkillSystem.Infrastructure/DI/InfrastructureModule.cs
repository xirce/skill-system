using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Departments;
using SkillSystem.Application.Repositories.Duties;
using SkillSystem.Application.Repositories.Employees;
using SkillSystem.Application.Repositories.Grades;
using SkillSystem.Application.Repositories.Positions;
using SkillSystem.Application.Repositories.Projects;
using SkillSystem.Application.Repositories.Roles;
using SkillSystem.Application.Repositories.Salaries;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.Employees;
using SkillSystem.Infrastructure.Persistence;
using SkillSystem.Infrastructure.Persistence.Repositories;
using SkillSystem.Infrastructure.Services;

namespace SkillSystem.Infrastructure.DI;

public class InfrastructureModule : Module
{
    private readonly IConfiguration configuration;

    public InfrastructureModule(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void Load(ContainerBuilder builder)
    {
        RegisterDbContext(builder);

        builder.RegisterType<SkillSystemDbInitializer>();

        builder.RegisterType<UnitOfWork>().AsImplementedInterfaces();

        builder.RegisterType<RolesRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
        builder.RegisterType<GradesRepository>().AsImplementedInterfaces();
        builder.RegisterType<SkillsRepository>().AsImplementedInterfaces();

        builder.RegisterType<PositionsRepository>().AsImplementedInterfaces();
        builder.RegisterType<DutiesRepository>().AsImplementedInterfaces();

        builder.RegisterType<ProjectsRepository>().AsImplementedInterfaces();
        builder.RegisterType<ProjectRolesRepository>().AsImplementedInterfaces();
        builder.RegisterType<DepartmentsRepository>().AsImplementedInterfaces();

        builder.RegisterType<EmployeesRepository>().AsImplementedInterfaces();

        builder.RegisterType<EmployeeSkillsRepository>().AsImplementedInterfaces();

        builder.RegisterType<SalariesRepository>().AsImplementedInterfaces();

        builder.RegisterType<EmployeesService>().AsImplementedInterfaces();
    }

    private void RegisterDbContext(ContainerBuilder builder)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SkillSystemDbContext>()
            .UseNpgsql(configuration.GetConnectionString(nameof(SkillSystem)));

        builder.RegisterInstance(optionsBuilder.Options);

        builder.RegisterType<SkillSystemDbContext>()
            .AsSelf()
            .InstancePerLifetimeScope();
    }
}
