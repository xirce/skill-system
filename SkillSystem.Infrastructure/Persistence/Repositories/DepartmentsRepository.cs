using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Departments;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly SkillSystemDbContext dbContext;

    public DepartmentsRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateDepartment(Department department)
    {
        await dbContext.Departments.AddAsync(department);
    }

    public async Task<Department?> FindDepartmentById(int departmentId)
    {
        return await dbContext.Departments.FindAsync(departmentId);
    }

    public async Task<Department?> FindDepartmentByName(string departmentName)
    {
        return await dbContext.Departments.FirstOrDefaultAsync(department => department.Name == departmentName);
    }

    public async Task<Department> GetDepartmentById(int departmentId)
    {
        return await FindDepartmentById(departmentId)
               ?? throw new EntityNotFoundException(nameof(Department), departmentId);
    }

    public async Task<IReadOnlyCollection<Employee>> GetDepartmentEmployees(int departmentId)
    {
        var departmentEmployees = await dbContext.Departments
            .Where(department => department.Id == departmentId)
            .Select(department => department.Employees)
            .FirstOrDefaultAsync();

        if (departmentEmployees is null)
            throw new EntityNotFoundException(nameof(Department), departmentId);

        return departmentEmployees.ToArray();
    }

    public async Task<IReadOnlyCollection<Department>> GetAllDepartments()
    {
        return await dbContext.Departments
            .OrderBy(department => department.Name)
            .ToListAsync();
    }

    public async Task AddEmployeeToDepartment(int departmentId, Employee employee)
    {
        var department = await GetDepartmentById(departmentId);

        var employeeInDepartment = new EmployeeInDepartment(department.Id, employee.Id);

        await dbContext.EmployeesInDepartments.AddAsync(employeeInDepartment);
    }

    public async Task RemoveEmployeeFromDepartment(int departmentId, Guid employeeId)
    {
        var employeeInDepartment = await dbContext.EmployeesInDepartments.FindAsync(departmentId, employeeId);

        if (employeeInDepartment is not null)
            dbContext.EmployeesInDepartments.Remove(employeeInDepartment);
    }

    public void UpdateDepartment(Department department)
    {
        dbContext.Departments.Update(department);
    }

    public void DeleteDepartment(Department department)
    {
        dbContext.Departments.Remove(department);
    }
}
