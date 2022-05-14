using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedChatJoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Users_InitiatorUid",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_InitiatorUid",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "InitiatorUid",
                table: "Chat");

            migrationBuilder.CreateTable(
                name: "UserChat",
                columns: table => new
                {
                    ChatsChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantsUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChat", x => new { x.ChatsChatId, x.ParticipantsUid });
                    table.ForeignKey(
                        name: "FK_UserChat_Chat_ChatsChatId",
                        column: x => x.ChatsChatId,
                        principalTable: "Chat",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChat_Users_ParticipantsUid",
                        column: x => x.ParticipantsUid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChat_ParticipantsUid",
                table: "UserChat",
                column: "ParticipantsUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChat");

            migrationBuilder.AddColumn<Guid>(
                name: "InitiatorUid",
                table: "Chat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Chat_InitiatorUid",
                table: "Chat",
                column: "InitiatorUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Users_InitiatorUid",
                table: "Chat",
                column: "InitiatorUid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
