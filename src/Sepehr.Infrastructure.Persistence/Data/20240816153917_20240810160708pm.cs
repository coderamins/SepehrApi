using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810160708pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoAnnounceDetails_CargoAnnounces_CargoAnnounceId",
                schema: "sepdb",
                table: "CargoAnnounceDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoAnnounceDetails_CargoAnnounces_CargoAnnounceId",
                schema: "sepdb",
                table: "CargoAnnounceDetails",
                column: "CargoAnnounceId",
                principalSchema: "sepdb",
                principalTable: "CargoAnnounces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoAnnounceDetails_CargoAnnounces_CargoAnnounceId",
                schema: "sepdb",
                table: "CargoAnnounceDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoAnnounceDetails_CargoAnnounces_CargoAnnounceId",
                schema: "sepdb",
                table: "CargoAnnounceDetails",
                column: "CargoAnnounceId",
                principalSchema: "sepdb",
                principalTable: "CargoAnnounces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
