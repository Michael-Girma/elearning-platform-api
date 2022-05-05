using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedPaymentOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineSession_TutorRequests_TutorRequestId",
                table: "OnlineSession");

            migrationBuilder.DropIndex(
                name: "IX_OnlineSession_SessionId",
                table: "OnlineSession");

            migrationBuilder.DropIndex(
                name: "IX_OnlineSession_TutorRequestId",
                table: "OnlineSession");

            migrationBuilder.DropColumn(
                name: "TutorRequestId",
                table: "OnlineSession");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedTime",
                table: "Sessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "PaymentOrders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Admins",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Admins",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_SessionId",
                table: "OnlineSession",
                column: "SessionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OnlineSession_SessionId",
                table: "OnlineSession");

            migrationBuilder.DropColumn(
                name: "BookedTime",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "PaymentOrders");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Admins");

            migrationBuilder.AddColumn<Guid>(
                name: "TutorRequestId",
                table: "OnlineSession",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_SessionId",
                table: "OnlineSession",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_TutorRequestId",
                table: "OnlineSession",
                column: "TutorRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineSession_TutorRequests_TutorRequestId",
                table: "OnlineSession",
                column: "TutorRequestId",
                principalTable: "TutorRequests",
                principalColumn: "TutorRequestId");
        }
    }
}
