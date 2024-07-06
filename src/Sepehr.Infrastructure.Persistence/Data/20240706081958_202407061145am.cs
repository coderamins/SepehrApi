using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202407061145am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIntermediary",
                schema: "sepdb",
                table: "PurchaseOrder");

            migrationBuilder.DropColumn(
                name: "CustomerCharacteristics",
                schema: "sepdb",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "InventotyCriticalPoint",
                schema: "sepdb",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxInventory",
                schema: "sepdb",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinInventory",
                schema: "sepdb",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventotyCriticalPoint",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MaxInventory",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinInventory",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsIntermediary",
                schema: "sepdb",
                table: "PurchaseOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CustomerCharacteristics",
                schema: "sepdb",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
