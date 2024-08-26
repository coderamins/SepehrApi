using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408260220pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatus",
                schema: "sepdb",
                table: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "OrderStatus",
                schema: "sepdb",
                newName: "OrderStatuses",
                newSchema: "sepdb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatuses",
                schema: "sepdb",
                table: "OrderStatuses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                schema: "sepdb",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                schema: "sepdb",
                table: "Orders",
                column: "OrderStatusId",
                principalSchema: "sepdb",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatuses",
                schema: "sepdb",
                table: "OrderStatuses");

            migrationBuilder.RenameTable(
                name: "OrderStatuses",
                schema: "sepdb",
                newName: "OrderStatus",
                newSchema: "sepdb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatus",
                schema: "sepdb",
                table: "OrderStatus",
                column: "Id");
        }
    }
}
