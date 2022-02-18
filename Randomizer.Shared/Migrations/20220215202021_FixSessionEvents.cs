using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRandomizer.Migrations
{
    public partial class FixSessionEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionEvents_SessionId",
                table: "SessionEvents");

            migrationBuilder.CreateIndex(
                name: "IX_SessionEvents_SessionId",
                table: "SessionEvents",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionEvents_SessionId",
                table: "SessionEvents");

            migrationBuilder.CreateIndex(
                name: "IX_SessionEvents_SessionId",
                table: "SessionEvents",
                column: "SessionId",
                unique: true);
        }
    }
}
