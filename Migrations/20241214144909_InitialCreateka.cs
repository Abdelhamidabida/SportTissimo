using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportissimoProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotDePasse",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MotDePasse",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
