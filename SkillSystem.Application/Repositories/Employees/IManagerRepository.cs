using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Employees;

public interface IManagerRepository
{
    Task SetManagerForEmployeeAsync(Guid employeeId, Guid managerId);
    Task<ManagerSubordinate?> FindEmployeeManagerAsync(Guid employeeId);
    Task RemoveManagerFromEmployeeAsync(Guid employeeId);
}
