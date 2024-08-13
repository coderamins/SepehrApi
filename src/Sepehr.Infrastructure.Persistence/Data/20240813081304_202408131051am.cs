using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408131051am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferWarehouseInventories_ProductBrands_ProductBrandId",
                schema: "sepdb",
                table: "TransferWarehouseInventories");

            migrationBuilder.DropIndex(
                name: "IX_TransferWarehouseInventories_ProductBrandId",
                schema: "sepdb",
                table: "TransferWarehouseInventories");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "sepdb",
                table: "TransferWarehouseInventories");

            migrationBuilder.DropColumn(
                name: "ProductBrandId",
                schema: "sepdb",
                table: "TransferWarehouseInventories");

            migrationBuilder.DropColumn(
                name: "ProductBrantId",
                schema: "sepdb",
                table: "TransferWarehouseInventories");

            migrationBuilder.DropColumn(
                name: "PaymentReason",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.AddColumn<int>(
                name: "PaymentRequestCode",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentRequestReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_PaymentRequestReasons_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentRequestReasonId",
                principalSchema: "sepdb",
                principalTable: "PaymentRequestReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_PaymentRequestReasons_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentRequestCode",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductBrandId",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductBrantId",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentReason",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseInventories_ProductBrandId",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                column: "ProductBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferWarehouseInventories_ProductBrands_ProductBrandId",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                column: "ProductBrandId",
                principalSchema: "sepdb",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
