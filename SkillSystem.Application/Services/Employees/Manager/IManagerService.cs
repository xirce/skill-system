using SkillSystem.Application.Services.Employees.Manager.Models;

namespace SkillSystem.Application.Services.Employees.Manager;

public interface IManagerService
{
    Task SetManagerForEmployeeAsync(SetManagerForEmployeeRequest request);
    Task RemoveManagerFromEmployeeAsync(Guid employeeId);
}
