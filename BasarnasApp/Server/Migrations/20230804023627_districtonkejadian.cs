using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasarnasApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class districtonkejadian : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Kejadian",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Kejadian_DistrictId",
                table: "Kejadian",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kejadian_Districts_DistrictId",
                table: "Kejadian",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kejadian_Districts_DistrictId",
                table: "Kejadian");

            migrationBuilder.DropIndex(
                name: "IX_Kejadian_DistrictId",
                table: "Kejadian");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Kejadian");
        }
    }
}
