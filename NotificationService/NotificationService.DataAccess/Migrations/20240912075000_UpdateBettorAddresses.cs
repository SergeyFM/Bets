using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBettorAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MessengerId",
                table: "BettorAddresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BettorAddresses_MessengerId",
                table: "BettorAddresses",
                column: "MessengerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BettorAddresses_Messengers_MessengerId",
                table: "BettorAddresses",
                column: "MessengerId",
                principalTable: "Messengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BettorAddresses_Messengers_MessengerId",
                table: "BettorAddresses");

            migrationBuilder.DropIndex(
                name: "IX_BettorAddresses_MessengerId",
                table: "BettorAddresses");

            migrationBuilder.DropColumn(
                name: "MessengerId",
                table: "BettorAddresses");
        }
    }
}
