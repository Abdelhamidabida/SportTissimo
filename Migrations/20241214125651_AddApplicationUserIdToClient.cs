using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportissimoProject.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserIdToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
    name: "ApplicationUserId",
    table: "Clients",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_ApplicationUserId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Clients");
        }
    }
}
