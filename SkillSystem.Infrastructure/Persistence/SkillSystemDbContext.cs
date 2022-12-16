using Microsoft.EntityFrameworkCore;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence;

public class SkillSystemDbContext : DbContext
{
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<GradeSkill> GradeSkills { get; set; }

    public SkillSystemDbContext()
    {
    }

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

        modelBuilder.Entity<Role>()
            .HasMany(role => role.Grades)
            .WithOne(grade => grade.Role)
            .HasForeignKey(grade => grade.RoleId);

        modelBuilder.Entity<Grade>(
            entity =>
            {
                entity
                    .HasMany(grade => grade.Skills)
                    .WithMany(skill => skill.Grades)
                    .UsingEntity<GradeSkill>(
                        joinEntity =>
                        {
                            joinEntity
                                .HasKey(gradeSkill => new { gradeSkill.GradeId, gradeSkill.SkillId });

                            joinEntity
                                .HasOne(gradeSkill => gradeSkill.Grade)
                                .WithMany(grade => grade.GradeSkills)
                                .HasForeignKey(gradeSkill => gradeSkill.GradeId);

                            joinEntity
                                .HasOne(gradeSkill => gradeSkill.Skill)
                                .WithMany(skill => skill.GradeSkills)
                                .HasForeignKey(gradeSkill => gradeSkill.SkillId)
                                .OnDelete(DeleteBehavior.Restrict);
                        }
                    );

                entity
                    .HasOne(grade => grade.PrevGrade)
                    .WithOne(prevGrade => prevGrade.NextGrade)
                    .HasForeignKey<Grade>(grade => grade.PrevGradeId)
                    .OnDelete(DeleteBehavior.SetNull);
            }
        );
    }
}