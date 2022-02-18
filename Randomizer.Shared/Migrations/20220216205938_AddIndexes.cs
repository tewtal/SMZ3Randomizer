using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRandomizer.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Guid",
                table: "Sessions",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_SessionEvents_ToWorldId_EventType",
                table: "SessionEvents",
                columns: new[] { "ToWorldId", "EventType" });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ConnectionId",
                table: "Clients",
                column: "ConnectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sessions_Guid",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_SessionEvents_ToWorldId_EventType",
                table: "SessionEvents");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ConnectionId",
                table: "Clients");
        }
    }
}
