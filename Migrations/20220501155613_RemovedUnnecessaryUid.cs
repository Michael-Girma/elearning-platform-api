using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class RemovedUnnecessaryUid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_CreatedByUserUid",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_CreatedByUserUid",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUid",
                table: "Subjects");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CreatedBy",
                table: "Subjects",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Users_CreatedBy",
                table: "Subjects",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_CreatedBy",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_CreatedBy",
                table: "Subjects");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserUid",
                table: "Subjects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CreatedByUserUid",
                table: "Subjects",
                column: "CreatedByUserUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Users_CreatedByUserUid",
                table: "Subjects",
                column: "CreatedByUserUid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
