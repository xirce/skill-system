using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSystem.Infrastructure.Migrations
{
    public partial class EmployeeIdToGuidMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE ""EmployeeSkills"" ALTER COLUMN ""EmployeeId"" TYPE uuid
                                   USING ""EmployeeId""::uuid");

            migrationBuilder.Sql(@"DELETE FROM ""EmployeeSkills""
                                   WHERE ""EmployeeId"" NOT IN (SELECT ""Id"" FROM ""Employees"")");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSkills_Employees_EmployeeId",
                table: "EmployeeSkills",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSkills_Employees_EmployeeId",
                table: "EmployeeSkills");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "EmployeeSkills",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
