using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elearning_platform.Migrations
{
    public partial class addPreferredSessionCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredSessionCount",
                table: "TaughtSubjects",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredSessionCount",
                table: "TaughtSubjects");
        }
    }
}
