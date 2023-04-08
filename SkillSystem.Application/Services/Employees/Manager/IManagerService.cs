using SkillSystem.Application.Services.Employees.Manager.Models;

namespace SkillSystem.Application.Services.Employees.Manager;

public interface IManagerService
{
    Task SetManagerAsync(SetManagerRequest request);
    Task RemoveManagerFromEmployeeAsync(Guid employeeId);
}
