using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRandomizer.Migrations
{
    public partial class WorldId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "Clients",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "Clients");
        }
    }
}
