using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class addedRecommendedSessionLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "ChatMessages",
                newName: "SenderId");

            migrationBuilder.AddColumn<int>(
                name: "PreferredSessionLength",
                table: "TaughtSubjects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderUid",
                table: "ChatMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderUid",
                table: "ChatMessages",
                column: "SenderUid");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Users_SenderUid",
                table: "ChatMessages",
                column: "SenderUid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_SenderUid",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_SenderUid",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "PreferredSessionLength",
                table: "TaughtSubjects");

            migrationBuilder.DropColumn(
                name: "SenderUid",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "ChatMessages",
                newName: "Sender");
        }
    }
}
