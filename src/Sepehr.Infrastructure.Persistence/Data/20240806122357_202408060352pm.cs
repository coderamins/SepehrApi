using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408060352pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelNameCode",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                schema: "sepdb",
                table: "CustomerLabels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductBrandId",
                schema: "sepdb",
                table: "CustomerLabels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                schema: "sepdb",
                table: "CustomerLabels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabels_BrandId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabels_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "CustomerLabelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabels_ProductBrandId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabels_ProductId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabels_ProductTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabels_Brands_BrandId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "BrandId",
                principalSchema: "sepdb",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabels_CustomerLabelTypes_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "CustomerLabelTypeId",
                principalSchema: "sepdb",
                principalTable: "CustomerLabelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabels_ProductBrands_ProductBrandId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "ProductBrandId",
                principalSchema: "sepdb",
                principalTable: "ProductBrands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabels_ProductTypes_ProductTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "ProductTypeId",
                principalSchema: "sepdb",
                principalTable: "ProductTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabels_Products_ProductId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "ProductId",
                principalSchema: "sepdb",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabels_Brands_BrandId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabels_CustomerLabelTypes_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabels_ProductBrands_ProductBrandId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabels_ProductTypes_ProductTypeId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabels_Products_ProductId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLabels_BrandId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLabels_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLabels_ProductBrandId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLabels_ProductId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLabels_ProductTypeId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropColumn(
                name: "BrandId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropColumn(
                name: "ProductBrandId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.AddColumn<string>(
                name: "LabelNameCode",
                schema: "sepdb",
                table: "CustomerLabels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
