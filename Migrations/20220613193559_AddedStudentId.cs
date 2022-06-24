using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedStudentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "SessionFeedbacks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionFeedbacks_SessionId",
                table: "SessionFeedbacks",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionFeedbacks_StudentId",
                table: "SessionFeedbacks",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionFeedbacks_Sessions_SessionId",
                table: "SessionFeedbacks",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionFeedbacks_Students_StudentId",
                table: "SessionFeedbacks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionFeedbacks_Sessions_SessionId",
                table: "SessionFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionFeedbacks_Students_StudentId",
                table: "SessionFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_SessionFeedbacks_SessionId",
                table: "SessionFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_SessionFeedbacks_StudentId",
                table: "SessionFeedbacks");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "SessionFeedbacks");
        }
    }
}
