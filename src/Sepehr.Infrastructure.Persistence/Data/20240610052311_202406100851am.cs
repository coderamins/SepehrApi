using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202406100851am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseOrderId",
                schema: "sepdb",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PurchaseOrderId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "PurchaseOrderId",
                principalSchema: "sepdb",
                principalTable: "PurchaseOrder",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_PurchaseOrderId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                schema: "sepdb",
                table: "OrderDetails");
        }
    }
}
