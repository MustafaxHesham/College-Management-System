using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeManagementSystem.Migrations
{
    public partial class RebuildDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseProfessor");

            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.CreateTable(
                name: "coursesAndProfessors",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coursesAndProfessors", x => new { x.ProfessorId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_coursesAndProfessors_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_coursesAndProfessors_professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentsAndCourses",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsAndCourses", x => new { x.CourseId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentsAndCourses_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsAndCourses_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_coursesAndProfessors_CourseId",
                table: "coursesAndProfessors",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsAndCourses_StudentId",
                table: "StudentsAndCourses",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coursesAndProfessors");

            migrationBuilder.DropTable(
                name: "StudentsAndCourses");

            migrationBuilder.CreateTable(
                name: "CourseProfessor",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    professorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProfessor", x => new { x.CoursesId, x.professorsId });
                    table.ForeignKey(
                        name: "FK_CourseProfessor_courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseProfessor_professors_professorsId",
                        column: x => x.professorsId,
                        principalTable: "professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    studentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesId, x.studentsId });
                    table.ForeignKey(
                        name: "FK_CourseStudent_courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_students_studentsId",
                        column: x => x.studentsId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseProfessor_professorsId",
                table: "CourseProfessor",
                column: "professorsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_studentsId",
                table: "CourseStudent",
                column: "studentsId");
        }
    }
}
