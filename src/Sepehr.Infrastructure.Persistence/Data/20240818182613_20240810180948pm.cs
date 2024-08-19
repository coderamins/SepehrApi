using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810180948pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittances_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRemittances_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "PurchaseOrderId",
                principalSchema: "sepdb",
                principalTable: "PurchaseOrder",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferRemittances_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances");

            migrationBuilder.DropIndex(
                name: "IX_TransferRemittances_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances");
        }
    }
}
