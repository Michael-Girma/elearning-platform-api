using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class AddedStudentIdToFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionFeedbacks_Students_StudentId",
                table: "SessionFeedbacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "SessionFeedbacks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionFeedbacks_Students_StudentId",
                table: "SessionFeedbacks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionFeedbacks_Students_StudentId",
                table: "SessionFeedbacks");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "SessionFeedbacks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionFeedbacks_Students_StudentId",
                table: "SessionFeedbacks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
