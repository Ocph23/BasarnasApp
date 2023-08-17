using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BasarnasApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class penaganan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PihakTerkait_Kejadian_KejadianId",
                table: "PihakTerkait");

            migrationBuilder.DropIndex(
                name: "IX_PihakTerkait_KejadianId",
                table: "PihakTerkait");

            migrationBuilder.DropColumn(
                name: "KejadianId",
                table: "PihakTerkait");

            migrationBuilder.CreateTable(
                name: "Penanganan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PihakTerkaitId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Deskripsi = table.Column<string>(type: "text", nullable: true),
                    KejadianId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penanganan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penanganan_Kejadian_KejadianId",
                        column: x => x.KejadianId,
                        principalTable: "Kejadian",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Penanganan_PihakTerkait_PihakTerkaitId",
                        column: x => x.PihakTerkaitId,
                        principalTable: "PihakTerkait",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Penanganan_KejadianId",
                table: "Penanganan",
                column: "KejadianId");

            migrationBuilder.CreateIndex(
                name: "IX_Penanganan_PihakTerkaitId",
                table: "Penanganan",
                column: "PihakTerkaitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Penanganan");

            migrationBuilder.AddColumn<int>(
                name: "KejadianId",
                table: "PihakTerkait",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PihakTerkait_KejadianId",
                table: "PihakTerkait",
                column: "KejadianId");

            migrationBuilder.AddForeignKey(
                name: "FK_PihakTerkait_Kejadian_KejadianId",
                table: "PihakTerkait",
                column: "KejadianId",
                principalTable: "Kejadian",
                principalColumn: "Id");
        }
    }
}
