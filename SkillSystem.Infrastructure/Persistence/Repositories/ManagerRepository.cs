using Microsoft.EntityFrameworkCore;
using SkillSystem.Application.Repositories.Employees;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly SkillSystemDbContext dbContext;

    public ManagerRepository(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task SetManagerForEmployeeAsync(Guid employeeId, Guid managerId)
    {
        var presentManager = await FindEmployeeManagerAsync(employeeId);
        if (presentManager is not null)
        {
            if (presentManager.ManagerId == managerId)
                return;
            dbContext.ManagersSubordinates.Remove(presentManager);
        }

        await dbContext.ManagersSubordinates.AddAsync(
            new ManagerSubordinate { SubordinateId = employeeId, ManagerId = managerId });
        await dbContext.SaveChangesAsync();
    }

    public async Task<ManagerSubordinate?> FindEmployeeManagerAsync(Guid employeeId)
    {
        return await dbContext.ManagersSubordinates.FirstOrDefaultAsync(
            managerSubordinate => managerSubordinate.SubordinateId == employeeId);
    }

    public async Task RemoveManagerFromEmployeeAsync(Guid employeeId)
    {
        var presentManager = await FindEmployeeManagerAsync(employeeId);
        if (presentManager is null)
            return;

        dbContext.ManagersSubordinates.Remove(presentManager);
        await dbContext.SaveChangesAsync();
    }
}
