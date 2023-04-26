using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSystem.Infrastructure.Migrations
{
    public partial class EmployeeSkillEnumStatusMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("IsApproved", "EmployeeSkills", "Status");

            migrationBuilder.Sql(
                @"ALTER TABLE ""EmployeeSkills"" ALTER COLUMN ""Status"" TYPE int
                  USING
                  CASE ""Status""
                      WHEN false  THEN 0
                      WHEN true  THEN 1
                  END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE ""EmployeeSkills"" ALTER COLUMN ""Status"" TYPE boolean
                  USING
                  CASE ""Status""
                      WHEN 0  THEN false
                      WHEN 1  THEN true
                  END");

            migrationBuilder.RenameColumn("Status", "EmployeeSkills", "IsApproved");
        }
    }
}
