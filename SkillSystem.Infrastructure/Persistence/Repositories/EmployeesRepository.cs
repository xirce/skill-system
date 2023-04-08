using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories.Employees;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly SkillSystemDbContext dbContext;

    public EmployeesRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Employee> GetOrCreateEmployee(Guid employeeId, Employee employee)
    {
        var presentEmployee = await FindEmployeeById(employeeId);
        if (presentEmployee is not null)
            return presentEmployee;

        await dbContext.AddAsync(employee);
        return employee;
    }

    public async Task CreateEmployees(IEnumerable<Employee> employees)
    {
        await dbContext.AddRangeAsync(employees);
    }

    public async Task<IReadOnlyCollection<Employee>> BatchGetEmployees(IEnumerable<Guid> employeesIds)
    {
        return await dbContext.Employees
            .AsNoTracking()
            .Where(employee => employeesIds.Contains(employee.Id))
            .ToListAsync();
    }

    public async Task<Employee?> FindEmployeeById(Guid employeeId)
    {
        return await dbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(employee => employee.Id == employeeId);
    }

    public async Task<Employee> GetEmployeeById(Guid employeeId)
    {
        return await FindEmployeeById(employeeId) ?? throw new EntityNotFoundException(nameof(Employee), employeeId);
    }

    public async Task<ICollection<Employee>> GetSubordinates(Guid managerId)
    {
        return await dbContext.Employees
            .AsNoTracking()
            .Where(employee => employee.ManagerId == managerId)
            .ToListAsync();
    }

    public void UpdateEmployee(Employee employee)
    {
        dbContext.Update(employee);
    }

    public void DeleteEmployee(Employee employee)
    {
        dbContext.Remove(employee);
    }
}
