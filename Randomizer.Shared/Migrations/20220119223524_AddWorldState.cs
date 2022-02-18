using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRandomizer.Migrations
{
    public partial class AddWorldState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorldState",
                table: "Worlds",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorldState",
                table: "Worlds");
        }
    }
}
