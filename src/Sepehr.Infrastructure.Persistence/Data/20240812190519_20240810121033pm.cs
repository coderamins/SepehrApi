using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810121033pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_WarehouseTypeId",
                schema: "sepdb",
                table: "Warehouses",
                column: "WarehouseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_WarehouseTypes_WarehouseTypeId",
                schema: "sepdb",
                table: "Warehouses",
                column: "WarehouseTypeId",
                principalSchema: "sepdb",
                principalTable: "WarehouseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_WarehouseTypes_WarehouseTypeId",
                schema: "sepdb",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_WarehouseTypeId",
                schema: "sepdb",
                table: "Warehouses");
        }
    }
}
