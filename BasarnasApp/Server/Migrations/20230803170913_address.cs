using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasarnasApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Alamat",
                table: "Pelapor",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Pelapor",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Pelapor");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Pelapor",
                newName: "Alamat");
        }
    }
}
