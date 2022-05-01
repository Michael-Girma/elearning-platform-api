using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserUid = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationLevelId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                    table.ForeignKey(
                        name: "FK_Subjects_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "EducationLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subjects_Users_CreatedByUserUid",
                        column: x => x.CreatedByUserUid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaughtSubjects",
                columns: table => new
                {
                    TaughtSubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    PricePerHour = table.Column<float>(type: "real", nullable: false),
                    TaughtOnline = table.Column<bool>(type: "boolean", nullable: false),
                    TaughtInPerson = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaughtSubjects", x => x.TaughtSubjectId);
                    table.ForeignKey(
                        name: "FK_TaughtSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaughtSubjects_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CreatedByUserUid",
                table: "Subjects",
                column: "CreatedByUserUid");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_EducationLevelId",
                table: "Subjects",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_TaughtSubjects_SubjectId",
                table: "TaughtSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TaughtSubjects_TutorId",
                table: "TaughtSubjects",
                column: "TutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaughtSubjects");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
