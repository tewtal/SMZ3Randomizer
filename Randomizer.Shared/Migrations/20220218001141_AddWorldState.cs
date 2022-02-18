using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRandomizer.Migrations
{
    public partial class AddWorldClientState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Worlds",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Worlds");
        }
    }
}
