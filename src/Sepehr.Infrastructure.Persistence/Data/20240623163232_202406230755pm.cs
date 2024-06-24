using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202406230755pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_AlternativeProductId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_Products_AlternativeProductId",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderDetails_AlternativeProductId",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_AlternativeProductId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "AlternativeProductId",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "AlternativeProductId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "AlternativeProductBrandId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlternativeProductBrandId",
                schema: "sepdb",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_AlternativeProductBrandId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "AlternativeProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AlternativeProductBrandId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "AlternativeProductBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductBrands_AlternativeProductBrandId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "AlternativeProductBrandId",
                principalSchema: "sepdb",
                principalTable: "ProductBrands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_ProductBrands_AlternativeProductBrandId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "AlternativeProductBrandId",
                principalSchema: "sepdb",
                principalTable: "ProductBrands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductBrands_AlternativeProductBrandId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_ProductBrands_AlternativeProductBrandId",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderDetails_AlternativeProductBrandId",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_AlternativeProductBrandId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "AlternativeProductBrandId",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "AlternativeProductBrandId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "AlternativeProductId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AlternativeProductId",
                schema: "sepdb",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_AlternativeProductId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "AlternativeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AlternativeProductId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "AlternativeProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_AlternativeProductId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "AlternativeProductId",
                principalSchema: "sepdb",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_Products_AlternativeProductId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "AlternativeProductId",
                principalSchema: "sepdb",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
