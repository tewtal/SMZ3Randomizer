using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRandomizer.Migrations
{
    public partial class AddConfirmedEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "SessionEvents",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "SessionEvents");
        }
    }
}
