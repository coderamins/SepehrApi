using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408301010pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentPaymentDetail_LadingExitPermit_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPaymentDetail_RentPayments_RentPaymentId",
                schema: "sepdb",
                table: "RentPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPaymentDetail_UnloadingPermits_UnloadingPermitId",
                schema: "sepdb",
                table: "RentPaymentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentPaymentDetail",
                schema: "sepdb",
                table: "RentPaymentDetail");

            migrationBuilder.RenameTable(
                name: "RentPaymentDetail",
                schema: "sepdb",
                newName: "RentPaymentDetails",
                newSchema: "sepdb");

            migrationBuilder.RenameIndex(
                name: "IX_RentPaymentDetail_UnloadingPermitId",
                schema: "sepdb",
                table: "RentPaymentDetails",
                newName: "IX_RentPaymentDetails_UnloadingPermitId");

            migrationBuilder.RenameIndex(
                name: "IX_RentPaymentDetail_RentPaymentId",
                schema: "sepdb",
                table: "RentPaymentDetails",
                newName: "IX_RentPaymentDetails_RentPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_RentPaymentDetail_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPaymentDetails",
                newName: "IX_RentPaymentDetails_LadingExitPermitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentPaymentDetails",
                schema: "sepdb",
                table: "RentPaymentDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPaymentDetails_LadingExitPermit_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPaymentDetails",
                column: "LadingExitPermitId",
                principalSchema: "sepdb",
                principalTable: "LadingExitPermit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPaymentDetails_RentPayments_RentPaymentId",
                schema: "sepdb",
                table: "RentPaymentDetails",
                column: "RentPaymentId",
                principalSchema: "sepdb",
                principalTable: "RentPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentPaymentDetails_UnloadingPermits_UnloadingPermitId",
                schema: "sepdb",
                table: "RentPaymentDetails",
                column: "UnloadingPermitId",
                principalSchema: "sepdb",
                principalTable: "UnloadingPermits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentPaymentDetails_LadingExitPermit_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPaymentDetails_RentPayments_RentPaymentId",
                schema: "sepdb",
                table: "RentPaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPaymentDetails_UnloadingPermits_UnloadingPermitId",
                schema: "sepdb",
                table: "RentPaymentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentPaymentDetails",
                schema: "sepdb",
                table: "RentPaymentDetails");

            migrationBuilder.RenameTable(
                name: "RentPaymentDetails",
                schema: "sepdb",
                newName: "RentPaymentDetail",
                newSchema: "sepdb");

            migrationBuilder.RenameIndex(
                name: "IX_RentPaymentDetails_UnloadingPermitId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                newName: "IX_RentPaymentDetail_UnloadingPermitId");

            migrationBuilder.RenameIndex(
                name: "IX_RentPaymentDetails_RentPaymentId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                newName: "IX_RentPaymentDetail_RentPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_RentPaymentDetails_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                newName: "IX_RentPaymentDetail_LadingExitPermitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentPaymentDetail",
                schema: "sepdb",
                table: "RentPaymentDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPaymentDetail_LadingExitPermit_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                column: "LadingExitPermitId",
                principalSchema: "sepdb",
                principalTable: "LadingExitPermit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPaymentDetail_RentPayments_RentPaymentId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                column: "RentPaymentId",
                principalSchema: "sepdb",
                principalTable: "RentPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RentPaymentDetail_UnloadingPermits_UnloadingPermitId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                column: "UnloadingPermitId",
                principalSchema: "sepdb",
                principalTable: "UnloadingPermits",
                principalColumn: "Id");
        }
    }
}
