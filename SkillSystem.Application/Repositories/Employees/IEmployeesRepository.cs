using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Employees;

public interface IEmployeesRepository
{
    Task<Employee> GetOrCreateEmployee(Guid employeeId, Employee employee);
    Task CreateEmployees(IEnumerable<Employee> employees);
    Task<IReadOnlyCollection<Employee>> BatchGetEmployees(IEnumerable<Guid> employeesIds);
    Task<Employee?> FindEmployeeById(Guid employeeId);
    Task<Employee> GetEmployeeById(Guid employeeId);
    Task<ICollection<Employee>> GetSubordinates(Guid managerId);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(Employee employee);
}
