using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810110936pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "sepdb",
                table: "WarehouseTypes");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "sepdb",
                table: "WarehouseTypes",
                newName: "WarehouseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WarehouseTypeId",
                schema: "sepdb",
                table: "WarehouseTypes",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "sepdb",
                table: "WarehouseTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
