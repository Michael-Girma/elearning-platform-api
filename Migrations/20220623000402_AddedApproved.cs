using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedApproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "TaughtSubjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_LessonDocuments_InternalFileMetadataId",
                table: "LessonDocuments",
                column: "InternalFileMetadataId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonDocuments_InternalFiles_InternalFileMetadataId",
                table: "LessonDocuments",
                column: "InternalFileMetadataId",
                principalTable: "InternalFiles",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonDocuments_InternalFiles_InternalFileMetadataId",
                table: "LessonDocuments");

            migrationBuilder.DropIndex(
                name: "IX_LessonDocuments_InternalFileMetadataId",
                table: "LessonDocuments");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "TaughtSubjects");
        }
    }
}
