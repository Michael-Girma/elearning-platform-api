using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class NullabilityFileMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalFiles_Users_UploadedByUid",
                table: "InternalFiles");

            migrationBuilder.AlterColumn<int>(
                name: "UploadedByUid",
                table: "InternalFiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalFiles_Users_UploadedByUid",
                table: "InternalFiles",
                column: "UploadedByUid",
                principalTable: "Users",
                principalColumn: "Uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalFiles_Users_UploadedByUid",
                table: "InternalFiles");

            migrationBuilder.AlterColumn<int>(
                name: "UploadedByUid",
                table: "InternalFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InternalFiles_Users_UploadedByUid",
                table: "InternalFiles",
                column: "UploadedByUid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
