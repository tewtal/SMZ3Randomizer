using Microsoft.EntityFrameworkCore.Migrations;

namespace WebRandomizer.Migrations
{
    public partial class ClientSeq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecievedSeq",
                table: "Clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SentSeq",
                table: "Clients",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecievedSeq",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SentSeq",
                table: "Clients");
        }
    }
}
