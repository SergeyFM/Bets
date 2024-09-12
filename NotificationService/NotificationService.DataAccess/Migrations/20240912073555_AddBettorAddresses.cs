using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBettorAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BettorAddresses_Bettors_BettorsId",
                table: "BettorAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_BettorAddresses_Messengers_MessengersId",
                table: "BettorAddresses");

            migrationBuilder.DropIndex(
                name: "IX_BettorAddresses_BettorsId",
                table: "BettorAddresses");

            migrationBuilder.DropColumn(
                name: "BettorsId",
                table: "BettorAddresses");

            migrationBuilder.RenameColumn(
                name: "MessengersId",
                table: "BettorAddresses",
                newName: "BettorId");

            migrationBuilder.RenameIndex(
                name: "IX_BettorAddresses_MessengersId",
                table: "BettorAddresses",
                newName: "IX_BettorAddresses_BettorId");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "BettorAddresses",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "BettorAddresses",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "BettorAddresses",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "BettorAddresses",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_BettorAddresses_Bettors_BettorId",
                table: "BettorAddresses",
                column: "BettorId",
                principalTable: "Bettors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BettorAddresses_Bettors_BettorId",
                table: "BettorAddresses");

            migrationBuilder.RenameColumn(
                name: "BettorId",
                table: "BettorAddresses",
                newName: "MessengersId");

            migrationBuilder.RenameIndex(
                name: "IX_BettorAddresses_BettorId",
                table: "BettorAddresses",
                newName: "IX_BettorAddresses_MessengersId");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "BettorAddresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "BettorAddresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "BettorAddresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "BettorAddresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<Guid>(
                name: "BettorsId",
                table: "BettorAddresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BettorAddresses_BettorsId",
                table: "BettorAddresses",
                column: "BettorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BettorAddresses_Bettors_BettorsId",
                table: "BettorAddresses",
                column: "BettorsId",
                principalTable: "Bettors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BettorAddresses_Messengers_MessengersId",
                table: "BettorAddresses",
                column: "MessengersId",
                principalTable: "Messengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
