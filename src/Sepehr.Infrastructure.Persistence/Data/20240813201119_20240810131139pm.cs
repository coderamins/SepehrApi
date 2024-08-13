using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810131139pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductInventories_ProductBrandId",
                schema: "sepdb",
                table: "ProductInventories");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_ProductBrandId_WarehouseId",
                schema: "sepdb",
                table: "ProductInventories",
                columns: new[] { "ProductBrandId", "WarehouseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_CustomerId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Customers_CustomerId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "CustomerId",
                principalSchema: "sepdb",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Customers_CustomerId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventories_ProductBrandId_WarehouseId",
                schema: "sepdb",
                table: "ProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_CustomerId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_ProductBrandId",
                schema: "sepdb",
                table: "ProductInventories",
                column: "ProductBrandId");
        }
    }
}
