using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class SwitchedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineSession_SessionOrders_SessionOrderId",
                table: "OnlineSession");

            migrationBuilder.DropIndex(
                name: "IX_OnlineSession_SessionOrderId",
                table: "OnlineSession");

            migrationBuilder.DropColumn(
                name: "PaymentOrderId",
                table: "OnlineSession");

            migrationBuilder.DropColumn(
                name: "SessionOrderId",
                table: "OnlineSession");

            migrationBuilder.AddColumn<Guid>(
                name: "OnlineSessionId",
                table: "SessionOrders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<float>(
                name: "TotalAmount",
                table: "PaymentDetails",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_OnlineSessionId",
                table: "SessionOrders",
                column: "OnlineSessionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionOrders_OnlineSession_OnlineSessionId",
                table: "SessionOrders",
                column: "OnlineSessionId",
                principalTable: "OnlineSession",
                principalColumn: "OnlineSessionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionOrders_OnlineSession_OnlineSessionId",
                table: "SessionOrders");

            migrationBuilder.DropIndex(
                name: "IX_SessionOrders_OnlineSessionId",
                table: "SessionOrders");

            migrationBuilder.DropColumn(
                name: "OnlineSessionId",
                table: "SessionOrders");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "PaymentDetails",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentOrderId",
                table: "OnlineSession",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SessionOrderId",
                table: "OnlineSession",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_SessionOrderId",
                table: "OnlineSession",
                column: "SessionOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineSession_SessionOrders_SessionOrderId",
                table: "OnlineSession",
                column: "SessionOrderId",
                principalTable: "SessionOrders",
                principalColumn: "SessionOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
