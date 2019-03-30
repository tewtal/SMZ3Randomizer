using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRandomizer.Migrations
{
    public partial class MoreWorldId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "Worlds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "Worlds");
        }
    }
}
