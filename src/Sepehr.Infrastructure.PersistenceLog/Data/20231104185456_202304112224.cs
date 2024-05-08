using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.PersistenceLog.Data
{
    /// <inheritdoc />
    public partial class _202304112224 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "ReceivePaymentSourceLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "ReceivePayLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductSupplierLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductPriceLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductInventorieLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductDetailLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductBrandLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "OrderServiceLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "OrderPaymentLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "OrderLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "OrderDetailLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "LadingLicenseDetailLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "CustomerLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LogTypeId",
                schema: "seplog",
                table: "CargoAnnouncementLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LogType",
                schema: "seplog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePaymentSourceLogs_LogTypeId",
                schema: "seplog",
                table: "ReceivePaymentSourceLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePayLogs_LogTypeId",
                schema: "seplog",
                table: "ReceivePayLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSupplierLogs_LogTypeId",
                schema: "seplog",
                table: "ProductSupplierLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceLogs_LogTypeId",
                schema: "seplog",
                table: "ProductPriceLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLogs_LogTypeId",
                schema: "seplog",
                table: "ProductLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventorieLogs_LogTypeId",
                schema: "seplog",
                table: "ProductInventorieLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetailLogs_LogTypeId",
                schema: "seplog",
                table: "ProductDetailLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrandLogs_LogTypeId",
                schema: "seplog",
                table: "ProductBrandLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServiceLogs_LogTypeId",
                schema: "seplog",
                table: "OrderServiceLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentLogs_LogTypeId",
                schema: "seplog",
                table: "OrderPaymentLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLogs_LogTypeId",
                schema: "seplog",
                table: "OrderLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailLogs_LogTypeId",
                schema: "seplog",
                table: "OrderDetailLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicenseDetailLogs_LogTypeId",
                schema: "seplog",
                table: "LadingLicenseDetailLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLogs_LogTypeId",
                schema: "seplog",
                table: "CustomerLogs",
                column: "LogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnouncementLogs_LogTypeId",
                schema: "seplog",
                table: "CargoAnnouncementLogs",
                column: "LogTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoAnnouncementLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "CargoAnnouncementLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "CustomerLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LadingLicenseDetailLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "LadingLicenseDetailLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "OrderDetailLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "OrderLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPaymentLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "OrderPaymentLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServiceLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "OrderServiceLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBrandLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductBrandLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetailLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductDetailLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventorieLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductInventorieLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPriceLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductPriceLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupplierLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductSupplierLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePayLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ReceivePayLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePaymentSourceLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ReceivePaymentSourceLogs",
                column: "LogTypeId",
                principalSchema: "seplog",
                principalTable: "LogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoAnnouncementLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "CargoAnnouncementLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "CustomerLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_LadingLicenseDetailLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "LadingLicenseDetailLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "OrderDetailLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "OrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderPaymentLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "OrderPaymentLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderServiceLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "OrderServiceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBrandLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductBrandLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetailLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductDetailLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventorieLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductInventorieLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPriceLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductPriceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupplierLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ProductSupplierLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePayLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ReceivePayLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePaymentSourceLogs_LogType_LogTypeId",
                schema: "seplog",
                table: "ReceivePaymentSourceLogs");

            migrationBuilder.DropTable(
                name: "LogType",
                schema: "seplog");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePaymentSourceLogs_LogTypeId",
                schema: "seplog",
                table: "ReceivePaymentSourceLogs");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePayLogs_LogTypeId",
                schema: "seplog",
                table: "ReceivePayLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductSupplierLogs_LogTypeId",
                schema: "seplog",
                table: "ProductSupplierLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductPriceLogs_LogTypeId",
                schema: "seplog",
                table: "ProductPriceLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductLogs_LogTypeId",
                schema: "seplog",
                table: "ProductLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventorieLogs_LogTypeId",
                schema: "seplog",
                table: "ProductInventorieLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductDetailLogs_LogTypeId",
                schema: "seplog",
                table: "ProductDetailLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProductBrandLogs_LogTypeId",
                schema: "seplog",
                table: "ProductBrandLogs");

            migrationBuilder.DropIndex(
                name: "IX_OrderServiceLogs_LogTypeId",
                schema: "seplog",
                table: "OrderServiceLogs");

            migrationBuilder.DropIndex(
                name: "IX_OrderPaymentLogs_LogTypeId",
                schema: "seplog",
                table: "OrderPaymentLogs");

            migrationBuilder.DropIndex(
                name: "IX_OrderLogs_LogTypeId",
                schema: "seplog",
                table: "OrderLogs");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailLogs_LogTypeId",
                schema: "seplog",
                table: "OrderDetailLogs");

            migrationBuilder.DropIndex(
                name: "IX_LadingLicenseDetailLogs_LogTypeId",
                schema: "seplog",
                table: "LadingLicenseDetailLogs");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLogs_LogTypeId",
                schema: "seplog",
                table: "CustomerLogs");

            migrationBuilder.DropIndex(
                name: "IX_CargoAnnouncementLogs_LogTypeId",
                schema: "seplog",
                table: "CargoAnnouncementLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "ReceivePaymentSourceLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "ReceivePayLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductSupplierLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductPriceLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductInventorieLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductDetailLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "ProductBrandLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "OrderServiceLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "OrderPaymentLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "OrderLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "OrderDetailLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "LadingLicenseDetailLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "CustomerLogs");

            migrationBuilder.DropColumn(
                name: "LogTypeId",
                schema: "seplog",
                table: "CargoAnnouncementLogs");
        }
    }
}
