using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectInIncomingMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "IncomingMessages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "IncomingMessages");
        }
    }
}
