using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasarnasApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class photo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Pelapor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumb",
                table: "Pelapor",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Pelapor");

            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Pelapor");
        }
    }
}
