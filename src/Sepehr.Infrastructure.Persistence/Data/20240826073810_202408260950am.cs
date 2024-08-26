using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408260950am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnloadingPermits_EntrancePermitId",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropIndex(
                name: "IX_LadingExitPermit_LadingPermitId",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "sepdb",
                table: "OrderStatuses");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "sepdb",
                table: "OrderStatuses",
                newName: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermits_EntrancePermitId",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "EntrancePermitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermit_LadingPermitId",
                schema: "sepdb",
                table: "LadingExitPermit",
                column: "LadingPermitId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnloadingPermits_EntrancePermitId",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropIndex(
                name: "IX_LadingExitPermit_LadingPermitId",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                schema: "sepdb",
                table: "OrderStatuses",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "sepdb",
                table: "OrderStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermits_EntrancePermitId",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "EntrancePermitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermit_LadingPermitId",
                schema: "sepdb",
                table: "LadingExitPermit",
                column: "LadingPermitId");
        }
    }
}
