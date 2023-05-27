using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Projects;
using SkillSystem.Application.Services.Projects.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/employees/{employeeId}/project-roles")]
public class EmployeeRolesController : BaseController
{
    private readonly IProjectRolesService projectRolesService;

    public EmployeeRolesController(IProjectRolesService projectRolesService)
    {
        this.projectRolesService = projectRolesService;
    }

    /// <summary>
    /// Получить по сотрудника его роли в проектах.
    /// </summary>
    /// <param name="employeeId">Идентификатор сотрудника</param>
    /// <param name="projectId">Идентификатор проекта</param>
    [HttpGet]
    public async Task<ICollection<EmployeeProjectRole>> GetEmployeeProjectRoles(
        Guid employeeId,
        [FromQuery] int? projectId = null)
    {
        return await projectRolesService.FindEmployeeProjectRoles(employeeId, projectId);
    }
}
