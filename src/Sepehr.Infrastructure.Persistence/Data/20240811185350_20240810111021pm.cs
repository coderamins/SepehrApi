using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810111021pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_WarehouseTypes_WarehouseTypeId",
                schema: "sepdb",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseTypes",
                schema: "sepdb",
                table: "WarehouseTypes");

            migrationBuilder.DropColumn(
                name: "WarehouseTypeId",
                schema: "sepdb",
                table: "WarehouseTypes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "sepdb",
                table: "WarehouseTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "sepdb",
                table: "WarehouseTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseTypes",
                schema: "sepdb",
                table: "WarehouseTypes",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseTypes",
                schema: "sepdb",
                table: "WarehouseTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "sepdb",
                table: "WarehouseTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "sepdb",
                table: "WarehouseTypes");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseTypeId",
                schema: "sepdb",
                table: "WarehouseTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseTypes",
                schema: "sepdb",
                table: "WarehouseTypes",
                column: "WarehouseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_WarehouseTypes_WarehouseTypeId",
                schema: "sepdb",
                table: "Warehouses",
                column: "WarehouseTypeId",
                principalSchema: "sepdb",
                principalTable: "WarehouseTypes",
                principalColumn: "WarehouseTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
