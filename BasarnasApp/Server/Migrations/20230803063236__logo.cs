using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasarnasApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class _logo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Instansi",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumb",
                table: "Instansi",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Instansi");

            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Instansi");
        }
    }
}
