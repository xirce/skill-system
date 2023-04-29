using SkillSystem.Application.Services.Departments.Models;

namespace SkillSystem.Application.Services.Departments;

public interface IDepartmentsService
{
    Task<int> CreateDepartment(CreateDepartmentRequest request);
    Task<DepartmentResponse> GetDepartmentById(int departmentId);
    Task<GetDepartmentEmployeesResponse> GetDepartmentEmployees(int departmentId);
    Task<IReadOnlyCollection<DepartmentResponse>> GetAllDepartments();
    Task AddEmployeeToDepartment(AddEmployeeToDepartmentRequest request);
    Task RemoveEmployeeFromDepartment(RemoveEmployeeFromDepartmentRequest request);
    Task SetHeadEmployee(SetHeadEmployeeRequest request);
    Task UpdateDepartment(UpdateDepartmentRequest request);
    Task DeleteDepartment(int departmentId);
}
