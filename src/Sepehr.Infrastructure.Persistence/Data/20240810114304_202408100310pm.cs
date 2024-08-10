using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408100310pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnloadingPermits_TransferRemittanceDetails_TransferRemittanceDetailId",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropIndex(
                name: "IX_UnloadingPermits_TransferRemittanceDetailId",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropColumn(
                name: "TransferRemittanceDetailId",
                schema: "sepdb",
                table: "UnloadingPermits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransferRemittanceDetailId",
                schema: "sepdb",
                table: "UnloadingPermits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermits_TransferRemittanceDetailId",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "TransferRemittanceDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnloadingPermits_TransferRemittanceDetails_TransferRemittanceDetailId",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "TransferRemittanceDetailId",
                principalSchema: "sepdb",
                principalTable: "TransferRemittanceDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
