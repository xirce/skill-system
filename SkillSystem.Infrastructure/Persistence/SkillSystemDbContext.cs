using Microsoft.EntityFrameworkCore;
using SkillSystem.Core.Entities;

namespace SkillSystem.Infrastructure.Persistence;

public class SkillSystemDbContext : DbContext
{
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Duty> Duties { get; set; }
    public DbSet<GradeSkill> GradeSkills { get; set; }
    public DbSet<PositionGrade> PositionGrades { get; set; }
    public DbSet<PositionDuty> PositionDuties { get; set; }
    public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
    public DbSet<ManagerSubordinate> ManagersSubordinates { get; set; }

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
                        });

                entity
                    .HasOne(grade => grade.PrevGrade)
                    .WithOne(prevGrade => prevGrade.NextGrade)
                    .HasForeignKey<Grade>(grade => grade.PrevGradeId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

        modelBuilder.Entity<Position>()
            .HasMany(position => position.MinGrades)
            .WithMany(grade => grade.Positions)
            .UsingEntity<PositionGrade>(
                joinEntity =>
                {
                    joinEntity
                        .HasKey(positionGrade => new { positionGrade.PositionId, positionGrade.GradeId });

                    joinEntity
                        .HasOne(positionGrade => positionGrade.Position)
                        .WithMany(position => position.PositionGrades)
                        .HasForeignKey(positionGrade => positionGrade.PositionId);

                    joinEntity
                        .HasOne(positionGrade => positionGrade.Grade)
                        .WithMany(grade => grade.PositionGrades)
                        .HasForeignKey(positionGrade => positionGrade.GradeId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

        modelBuilder.Entity<Position>()
            .HasMany(position => position.Duties)
            .WithMany(duty => duty.Positions)
            .UsingEntity<PositionDuty>(
                joinEntity =>
                {
                    joinEntity
                        .HasKey(positionDuty => new { positionDuty.PositionId, positionDuty.DutyId });

                    joinEntity
                        .HasOne(positionDuty => positionDuty.Position)
                        .WithMany(position => position.PositionDuties)
                        .HasForeignKey(positionDuty => positionDuty.PositionId);

                    joinEntity
                        .HasOne(positionDuty => positionDuty.Duty)
                        .WithMany(duty => duty.PositionDuties)
                        .HasForeignKey(positionDuty => positionDuty.DutyId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

        modelBuilder.Entity<EmployeeSkill>()
            .HasKey(skill => new { skill.EmployeeId, skill.SkillId });

        modelBuilder.Entity<EmployeeSkill>()
            .HasOne(skill => skill.Skill)
            .WithMany()
            .HasForeignKey(skill => skill.SkillId);

        modelBuilder.Entity<ManagerSubordinate>()
            .HasKey(managerSubordinate => new { managerSubordinate.ManagerId, managerSubordinate.SubordinateId });
    }
}
