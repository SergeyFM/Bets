using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSendingStatesInIncomingMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "IncomingMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "IncomingMessages");
        }
    }
}
