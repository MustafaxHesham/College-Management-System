using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeManagementSystem.Migrations
{
    public partial class AddingColumnHoursToStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPassedHours",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPassedHours",
                table: "students");
        }
    }
}
