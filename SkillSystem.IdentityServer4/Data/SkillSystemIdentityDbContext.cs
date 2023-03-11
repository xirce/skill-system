using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillSystem.IdentityServer4.Data.Entities;

namespace SkillSystem.IdentityServer4.Data;

public class SkillSystemIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public SkillSystemIdentityDbContext(DbContextOptions<SkillSystemIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("pg_trgm");

        builder.Entity<ApplicationUser>()
            .Property(user => user.FullName)
            .HasComputedColumnSql(
                $"lower(\"{nameof(ApplicationUser.LastName)}\""
                + $" || ' ' || \"{nameof(ApplicationUser.FirstName)}\""
                + $" || ' ' || \"{nameof(ApplicationUser.Patronymic)}\")",
                stored: true);

        builder.Entity<ApplicationUser>()
            .HasIndex(user => user.FullName)
            .HasMethod("gin")
            .HasOperators("gin_trgm_ops");
    }
}
