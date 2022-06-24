using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class IntermeditateryRemovedSenderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Users_SenderUid",
                table: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_SenderUid",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "SenderUid",
                table: "ChatMessages");

            migrationBuilder.AddColumn<bool>(
                name: "SeenByParticipant",
                table: "ChatMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeenByParticipant",
                table: "ChatMessages");

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "ChatMessages",
                type: "text",
                nullable: false,
                defaultValue: "");

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
    }
}
