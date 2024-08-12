using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810121158pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferWarehouseInventories",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBrantId = table.Column<int>(type: "int", nullable: false),
                    OriginWarehouseId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferWarehouseInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferWarehouseInventories_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferWarehouseInventories_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransferWarehouseInventoryDetails",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferWarehouseInventoryId = table.Column<int>(type: "int", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferWarehouseInventoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferWarehouseInventoryDetails_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferWarehouseInventoryDetails_TransferWarehouseInventories_TransferWarehouseInventoryId",
                        column: x => x.TransferWarehouseInventoryId,
                        principalSchema: "sepdb",
                        principalTable: "TransferWarehouseInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseInventories_CreatedBy",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseInventories_ProductBrandId",
                schema: "sepdb",
                table: "TransferWarehouseInventories",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseInventoryDetails_ProductBrandId",
                schema: "sepdb",
                table: "TransferWarehouseInventoryDetails",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseInventoryDetails_TransferWarehouseInventoryId",
                schema: "sepdb",
                table: "TransferWarehouseInventoryDetails",
                column: "TransferWarehouseInventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferWarehouseInventoryDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "TransferWarehouseInventories",
                schema: "sepdb");
        }
    }
}
