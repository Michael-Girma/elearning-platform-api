using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaughtSubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    InternalFileMetadataId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_LessonDocuments_TaughtSubjects_TaughtSubjectId",
                        column: x => x.TaughtSubjectId,
                        principalTable: "TaughtSubjects",
                        principalColumn: "TaughtSubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonDocuments_TaughtSubjectId",
                table: "LessonDocuments",
                column: "TaughtSubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonDocuments");
        }
    }
}
