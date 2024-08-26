using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408261109am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                schema: "sepdb",
                table: "OrderStatuses",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "sepdb",
                table: "OrderStatuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "sepdb",
                table: "OrderStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "sepdb",
                table: "OrderStatuses");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "sepdb",
                table: "OrderStatuses",
                newName: "OrderStatusId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusId",
                schema: "sepdb",
                table: "OrderStatuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
