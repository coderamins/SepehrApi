using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810120957pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseTypes",
                schema: "sepdb",
                table: "WarehouseTypes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseTypes",
                schema: "sepdb",
                table: "WarehouseTypes");

            migrationBuilder.DropColumn(
                name: "Id",
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
        }
    }
}
