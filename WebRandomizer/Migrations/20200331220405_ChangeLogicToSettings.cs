using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRandomizer.Migrations
{
    public partial class ChangeLogicToSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logic",
                table: "Worlds");

            migrationBuilder.AddColumn<string>(
                name: "Settings",
                table: "Worlds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Settings",
                table: "Worlds");

            migrationBuilder.AddColumn<string>(
                name: "Logic",
                table: "Worlds",
                type: "text",
                nullable: true);
        }
    }
}
