using Microsoft.EntityFrameworkCore;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence;

public class SkillSystemDbContext : DbContext
{
    public DbSet<Skill> Skills { get; set; }

    public SkillSystemDbContext(DbContextOptions<SkillSystemDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Skill>()
            .HasOne(skill => skill.Group)
            .WithMany(group => group.SubSkills)
            .HasForeignKey(skill => skill.GroupId);
    }
}