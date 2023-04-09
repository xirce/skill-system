using SkillSystem.Application.Repositories;

namespace SkillSystem.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly SkillSystemDbContext dbContext;

    public UnitOfWork(SkillSystemDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
