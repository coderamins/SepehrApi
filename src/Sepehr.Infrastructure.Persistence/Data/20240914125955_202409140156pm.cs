using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409140156pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_DraftOrders_DraftOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_DraftOrders_Users_CreatedBy",
                schema: "sepdb",
                table: "DraftOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DraftOrders_DraftOrderId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DraftOrders",
                schema: "sepdb",
                table: "DraftOrders");

            migrationBuilder.RenameTable(
                name: "DraftOrders",
                schema: "sepdb",
                newName: "DraftOrder",
                newSchema: "sepdb");

            migrationBuilder.RenameIndex(
                name: "IX_DraftOrders_CreatedBy",
                schema: "sepdb",
                table: "DraftOrder",
                newName: "IX_DraftOrder_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DraftOrder",
                schema: "sepdb",
                table: "DraftOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                schema: "sepdb",
                table: "Products",
                column: "ProductTypeId",
                unique: true,
                filter: "[ProductTypeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_DraftOrder_DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "DraftOrderId",
                principalSchema: "sepdb",
                principalTable: "DraftOrder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DraftOrder_Users_CreatedBy",
                schema: "sepdb",
                table: "DraftOrder",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DraftOrder_DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                column: "DraftOrderId",
                principalSchema: "sepdb",
                principalTable: "DraftOrder",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_DraftOrder_DraftOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_DraftOrder_Users_CreatedBy",
                schema: "sepdb",
                table: "DraftOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DraftOrder_DraftOrderId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DraftOrder",
                schema: "sepdb",
                table: "DraftOrder");

            migrationBuilder.RenameTable(
                name: "DraftOrder",
                schema: "sepdb",
                newName: "DraftOrders",
                newSchema: "sepdb");

            migrationBuilder.RenameIndex(
                name: "IX_DraftOrder_CreatedBy",
                schema: "sepdb",
                table: "DraftOrders",
                newName: "IX_DraftOrders_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DraftOrders",
                schema: "sepdb",
                table: "DraftOrders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                schema: "sepdb",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_DraftOrders_DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "DraftOrderId",
                principalSchema: "sepdb",
                principalTable: "DraftOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DraftOrders_Users_CreatedBy",
                schema: "sepdb",
                table: "DraftOrders",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DraftOrders_DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                column: "DraftOrderId",
                principalSchema: "sepdb",
                principalTable: "DraftOrders",
                principalColumn: "Id");
        }
    }
}
