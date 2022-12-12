using Microsoft.EntityFrameworkCore;
using Serilog;

namespace SkillSystem.IdentityServer4.Data;

public class SkillSystemIdentityDbInitializer
{
    private readonly SkillSystemIdentityDbContext dbContext;

    public SkillSystemIdentityDbInitializer(SkillSystemIdentityDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            Log.Error(exception, "An error occurred during database initialization");
            throw;
        }
    }
}