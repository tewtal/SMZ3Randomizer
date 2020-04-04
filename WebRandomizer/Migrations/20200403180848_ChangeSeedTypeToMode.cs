using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRandomizer.Migrations
{
    public partial class ChangeSeedTypeToMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Seeds");

            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Seeds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Seeds");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Seeds",
                type: "text",
                nullable: true);
        }
    }
}
