using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202404300922pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_OrderSendTypes_OrderSendTypeId",
                schema: "sepdb",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_PaymentTypes_FarePaymentTypeId",
                schema: "sepdb",
                table: "PurchaseOrder");

            migrationBuilder.CreateTable(
                name: "PurchaseOrderFarePaymentTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderFarePaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderSendTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SendTypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderSendTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_PurchaseOrderFarePaymentTypes_FarePaymentTypeId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "FarePaymentTypeId",
                principalSchema: "sepdb",
                principalTable: "PurchaseOrderFarePaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_PurchaseOrderSendTypes_OrderSendTypeId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "OrderSendTypeId",
                principalSchema: "sepdb",
                principalTable: "PurchaseOrderSendTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_PurchaseOrderFarePaymentTypes_FarePaymentTypeId",
                schema: "sepdb",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_PurchaseOrderSendTypes_OrderSendTypeId",
                schema: "sepdb",
                table: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "PurchaseOrderFarePaymentTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderSendTypes",
                schema: "sepdb");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_OrderSendTypes_OrderSendTypeId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "OrderSendTypeId",
                principalSchema: "sepdb",
                principalTable: "OrderSendTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_PaymentTypes_FarePaymentTypeId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "FarePaymentTypeId",
                principalSchema: "sepdb",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
