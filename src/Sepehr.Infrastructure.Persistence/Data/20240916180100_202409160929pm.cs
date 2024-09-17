using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409160929pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseOrderId",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "FareAmount",
                schema: "sepdb",
                table: "TransferRemittances",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualWeightedAverage",
                schema: "sepdb",
                table: "ProductInventories",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProximateWeightedAverage",
                schema: "sepdb",
                table: "ProductInventories",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseInventories_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferWarehouseInventories_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                column: "PurchaseOrderId",
                principalSchema: "sepdb",
                principalTable: "PurchaseOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferWarehouseInventories_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferWarehouseInventories");

            migrationBuilder.DropIndex(
                name: "IX_TransferWarehouseInventories_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferWarehouseInventories");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                schema: "sepdb",
                table: "TransferWarehouseInventories");

            migrationBuilder.DropColumn(
                name: "ActualWeightedAverage",
                schema: "sepdb",
                table: "ProductInventories");

            migrationBuilder.DropColumn(
                name: "ProximateWeightedAverage",
                schema: "sepdb",
                table: "ProductInventories");

            migrationBuilder.AlterColumn<decimal>(
                name: "FareAmount",
                schema: "sepdb",
                table: "TransferRemittances",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");
        }
    }
}
