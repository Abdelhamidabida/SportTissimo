using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportissimoProject.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserIdToClientsaqqqaqaaaaaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_ApplicationUserId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Clients",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Clients",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_ApplicationUserId",
                table: "Clients",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
