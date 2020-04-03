using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRandomizer.Migrations
{
    public partial class AddVersionToSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameVersion",
                table: "Seeds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameVersion",
                table: "Seeds");
        }
    }
}
