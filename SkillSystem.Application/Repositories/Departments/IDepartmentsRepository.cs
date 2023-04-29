using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Repositories.Departments;

public interface IDepartmentsRepository
{
    Task CreateDepartment(Department department);
    Task<Department?> FindDepartmentById(int departmentId);
    Task<Department?> FindDepartmentByName(string departmentName);
    Task<Department> GetDepartmentById(int departmentId);
    Task<IReadOnlyCollection<Employee>> GetDepartmentEmployees(int departmentId);
    Task<IReadOnlyCollection<Department>> GetAllDepartments();
    Task AddEmployeeToDepartment(int departmentId, Employee employee);
    Task RemoveEmployeeFromDepartment(int departmentId, Guid employeeId);
    void UpdateDepartment(Department department);
    void DeleteDepartment(Department department);
}
