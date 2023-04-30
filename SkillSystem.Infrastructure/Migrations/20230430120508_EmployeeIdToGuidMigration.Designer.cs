﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SkillSystem.Infrastructure.Persistence;

#nullable disable

namespace SkillSystem.Infrastructure.Migrations
{
    [DbContext(typeof(SkillSystemDbContext))]
    [Migration("20230430120508_EmployeeIdToGuidMigration")]
    partial class EmployeeIdToGuidMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SkillSystem.Core.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("HeadEmployeeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HeadEmployeeId")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Duty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Duties");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.EmployeeInDepartment", b =>
                {
                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.HasKey("DepartmentId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeesInDepartments");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.EmployeeSkill", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("EmployeeId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("EmployeeSkills");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("PrevGradeId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PrevGradeId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.GradeSkill", b =>
                {
                    b.Property<int>("GradeId")
                        .HasColumnType("integer");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.HasKey("GradeId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("GradeSkills");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.PositionDuty", b =>
                {
                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("DutyId")
                        .HasColumnType("integer");

                    b.HasKey("PositionId", "DutyId");

                    b.HasIndex("DutyId");

                    b.ToTable("PositionDuties");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.PositionGrade", b =>
                {
                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("GradeId")
                        .HasColumnType("integer");

                    b.HasKey("PositionId", "GradeId");

                    b.HasIndex("GradeId");

                    b.ToTable("PositionGrades");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.ProjectRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RoleId");

                    b.ToTable("ProjectRoles");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Bonus")
                        .HasColumnType("numeric");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Wage")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Department", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Employee", "HeadEmployee")
                        .WithOne()
                        .HasForeignKey("SkillSystem.Core.Entities.Department", "HeadEmployeeId");

                    b.Navigation("HeadEmployee");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Employee", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.EmployeeInDepartment", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillSystem.Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.EmployeeSkill", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillSystem.Core.Entities.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Grade", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Grade", "PrevGrade")
                        .WithOne("NextGrade")
                        .HasForeignKey("SkillSystem.Core.Entities.Grade", "PrevGradeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SkillSystem.Core.Entities.Role", "Role")
                        .WithMany("Grades")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrevGrade");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.GradeSkill", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Grade", "Grade")
                        .WithMany("GradeSkills")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillSystem.Core.Entities.Skill", "Skill")
                        .WithMany("GradeSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Grade");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.PositionDuty", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Duty", "Duty")
                        .WithMany("PositionDuties")
                        .HasForeignKey("DutyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkillSystem.Core.Entities.Position", "Position")
                        .WithMany("PositionDuties")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Duty");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.PositionGrade", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Grade", "Grade")
                        .WithMany("PositionGrades")
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SkillSystem.Core.Entities.Position", "Position")
                        .WithMany("PositionGrades")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.ProjectRole", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("SkillSystem.Core.Entities.Project", "Project")
                        .WithMany("Roles")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillSystem.Core.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Skill", b =>
                {
                    b.HasOne("SkillSystem.Core.Entities.Skill", "Group")
                        .WithMany("SubSkills")
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Duty", b =>
                {
                    b.Navigation("PositionDuties");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Grade", b =>
                {
                    b.Navigation("GradeSkills");

                    b.Navigation("NextGrade");

                    b.Navigation("PositionGrades");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Position", b =>
                {
                    b.Navigation("PositionDuties");

                    b.Navigation("PositionGrades");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Project", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Role", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("SkillSystem.Core.Entities.Skill", b =>
                {
                    b.Navigation("GradeSkills");

                    b.Navigation("SubSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
