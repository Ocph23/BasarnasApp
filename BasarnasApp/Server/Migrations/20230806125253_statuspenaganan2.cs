using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasarnasApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class statuspenaganan2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Penanganan_PihakTerkait_PihakTerkaitId",
                table: "Penanganan");

            migrationBuilder.AlterColumn<int>(
                name: "PihakTerkaitId",
                table: "Penanganan",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Penanganan_PihakTerkait_PihakTerkaitId",
                table: "Penanganan",
                column: "PihakTerkaitId",
                principalTable: "PihakTerkait",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Penanganan_PihakTerkait_PihakTerkaitId",
                table: "Penanganan");

            migrationBuilder.AlterColumn<int>(
                name: "PihakTerkaitId",
                table: "Penanganan",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Penanganan_PihakTerkait_PihakTerkaitId",
                table: "Penanganan",
                column: "PihakTerkaitId",
                principalTable: "PihakTerkait",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
