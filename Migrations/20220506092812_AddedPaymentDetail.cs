using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedPaymentDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineSession_PaymentOrders_PaymentOrderId",
                table: "OnlineSession");

            migrationBuilder.DropTable(
                name: "PaymentOrders");

            migrationBuilder.DropIndex(
                name: "IX_OnlineSession_PaymentOrderId",
                table: "OnlineSession");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Sessions");

            migrationBuilder.AddColumn<Guid>(
                name: "SessionOrderId",
                table: "OnlineSession",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaymentAccountDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    YenePaySellerCode = table.Column<string>(type: "text", nullable: true),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAccountDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAccountDetail_Users_Uid",
                        column: x => x.Uid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    MerchantOrderId = table.Column<string>(type: "text", nullable: false),
                    MerchantId = table.Column<string>(type: "text", nullable: false),
                    MerchantCode = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: false),
                    TransactionCode = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Signature = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionOrders",
                columns: table => new
                {
                    SessionOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: false),
                    PaymentDetailId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionOrders", x => x.SessionOrderId);
                    table.ForeignKey(
                        name: "FK_SessionOrders_PaymentDetails_PaymentDetailId",
                        column: x => x.PaymentDetailId,
                        principalTable: "PaymentDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SessionPaymentLinks",
                columns: table => new
                {
                    PaymentLinkId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OnSuccessReturn = table.Column<string>(type: "text", nullable: false),
                    OnCancelReturn = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<int>(type: "integer", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    MerchantCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPaymentLinks", x => x.PaymentLinkId);
                    table.ForeignKey(
                        name: "FK_SessionPaymentLinks_SessionOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "SessionOrders",
                        principalColumn: "SessionOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_SessionOrderId",
                table: "OnlineSession",
                column: "SessionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAccountDetail_Uid",
                table: "PaymentAccountDetail",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionOrders_PaymentDetailId",
                table: "SessionOrders",
                column: "PaymentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPaymentLinks_OrderId",
                table: "SessionPaymentLinks",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineSession_SessionOrders_SessionOrderId",
                table: "OnlineSession",
                column: "SessionOrderId",
                principalTable: "SessionOrders",
                principalColumn: "SessionOrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineSession_SessionOrders_SessionOrderId",
                table: "OnlineSession");

            migrationBuilder.DropTable(
                name: "PaymentAccountDetail");

            migrationBuilder.DropTable(
                name: "SessionPaymentLinks");

            migrationBuilder.DropTable(
                name: "SessionOrders");

            migrationBuilder.DropTable(
                name: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_OnlineSession_SessionOrderId",
                table: "OnlineSession");

            migrationBuilder.DropColumn(
                name: "SessionOrderId",
                table: "OnlineSession");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Sessions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PaymentOrders",
                columns: table => new
                {
                    PaymentOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOrders", x => x.PaymentOrderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnlineSession_PaymentOrderId",
                table: "OnlineSession",
                column: "PaymentOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineSession_PaymentOrders_PaymentOrderId",
                table: "OnlineSession",
                column: "PaymentOrderId",
                principalTable: "PaymentOrders",
                principalColumn: "PaymentOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
