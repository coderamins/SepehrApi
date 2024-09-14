using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409110147pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DraftOrderCode",
                schema: "sepdb",
                table: "Orders",
                newName: "DraftOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                column: "DraftOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DraftOrders_DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                column: "DraftOrderId",
                principalSchema: "sepdb",
                principalTable: "DraftOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DraftOrders_DraftOrderId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DraftOrderId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                newName: "DraftOrderCode");
        }
    }
}
