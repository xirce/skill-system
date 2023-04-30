using Autofac;
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

namespace SkillSystem.Application.DI;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<RolesService>().AsImplementedInterfaces();
        builder.RegisterType<GradesService>().AsImplementedInterfaces();
        builder.RegisterType<SkillsService>().AsImplementedInterfaces();

        builder.RegisterType<PositionsService>().AsImplementedInterfaces();
        builder.RegisterType<DutiesService>().AsImplementedInterfaces();

        builder.RegisterType<ProjectsService>().AsImplementedInterfaces();
        builder.RegisterType<ProjectRolesService>().AsImplementedInterfaces();
        builder.RegisterType<DepartmentsService>().AsImplementedInterfaces();

        builder.RegisterType<EmployeesManager>().AsImplementedInterfaces();
        builder.RegisterType<ManagerService>().AsImplementedInterfaces();

        builder.RegisterType<EmployeeSkillsService>().AsImplementedInterfaces();

        builder.RegisterType<SalariesService>().AsImplementedInterfaces();
    }
}
