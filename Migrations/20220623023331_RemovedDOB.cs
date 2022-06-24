using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class RemovedDOB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_EducationLevels_EducationLevelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "EducationLevelId",
                table: "Students",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_EducationLevels_EducationLevelId",
                table: "Students",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "EducationLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_EducationLevels_EducationLevelId",
                table: "Students");

            migrationBuilder.AlterColumn<Guid>(
                name: "EducationLevelId",
                table: "Students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Students_EducationLevels_EducationLevelId",
                table: "Students",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "EducationLevelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
