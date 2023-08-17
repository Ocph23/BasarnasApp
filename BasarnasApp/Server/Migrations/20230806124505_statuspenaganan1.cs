using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasarnasApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class statuspenaganan1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstansiId",
                table: "Penanganan",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Penanganan_InstansiId",
                table: "Penanganan",
                column: "InstansiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Penanganan_Instansi_InstansiId",
                table: "Penanganan",
                column: "InstansiId",
                principalTable: "Instansi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Penanganan_Instansi_InstansiId",
                table: "Penanganan");

            migrationBuilder.DropIndex(
                name: "IX_Penanganan_InstansiId",
                table: "Penanganan");

            migrationBuilder.DropColumn(
                name: "InstansiId",
                table: "Penanganan");
        }
    }
}
