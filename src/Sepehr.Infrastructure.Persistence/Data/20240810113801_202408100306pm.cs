using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408100306pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LadingExitPermitDetails_CargoAnnounceDetailId",
                schema: "sepdb",
                table: "LadingExitPermitDetails");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermitDetails_CargoAnnounceDetailId",
                schema: "sepdb",
                table: "LadingExitPermitDetails",
                column: "CargoAnnounceDetailId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LadingExitPermitDetails_CargoAnnounceDetailId",
                schema: "sepdb",
                table: "LadingExitPermitDetails");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermitDetails_CargoAnnounceDetailId",
                schema: "sepdb",
                table: "LadingExitPermitDetails",
                column: "CargoAnnounceDetailId");
        }
    }
}
