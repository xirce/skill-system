using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillSystem.Infrastructure.Persistence;

public class SkillSystemDbInitializer
{
    private readonly SkillSystemDbContext dbContext;
    private readonly ILogger<SkillSystemDbInitializer> logger;

    public SkillSystemDbInitializer(SkillSystemDbContext dbContext, ILogger<SkillSystemDbInitializer> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred during database initialization");
            throw;
        }
    }
}