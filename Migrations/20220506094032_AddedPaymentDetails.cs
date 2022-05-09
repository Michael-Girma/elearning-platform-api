using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedPaymentDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccountDetail_Users_Uid",
                table: "PaymentAccountDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentAccountDetail",
                table: "PaymentAccountDetail");

            migrationBuilder.RenameTable(
                name: "PaymentAccountDetail",
                newName: "PaymentAccountDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentAccountDetail_Uid",
                table: "PaymentAccountDetails",
                newName: "IX_PaymentAccountDetails_Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentAccountDetails",
                table: "PaymentAccountDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccountDetails_Users_Uid",
                table: "PaymentAccountDetails",
                column: "Uid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentAccountDetails_Users_Uid",
                table: "PaymentAccountDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentAccountDetails",
                table: "PaymentAccountDetails");

            migrationBuilder.RenameTable(
                name: "PaymentAccountDetails",
                newName: "PaymentAccountDetail");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentAccountDetails_Uid",
                table: "PaymentAccountDetail",
                newName: "IX_PaymentAccountDetail_Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentAccountDetail",
                table: "PaymentAccountDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentAccountDetail_Users_Uid",
                table: "PaymentAccountDetail",
                column: "Uid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
