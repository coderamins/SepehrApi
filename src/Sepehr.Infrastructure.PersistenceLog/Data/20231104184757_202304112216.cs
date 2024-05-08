using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.PersistenceLog.Data
{
    /// <inheritdoc />
    public partial class _202304112216 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "seplog");

            migrationBuilder.RenameTable(
                name: "TableRecordRemovalInfoLogs",
                schema: "sepdb",
                newName: "TableRecordRemovalInfoLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "ReceivePaymentSourceLogs",
                schema: "sepdb",
                newName: "ReceivePaymentSourceLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "ReceivePayLogs",
                schema: "sepdb",
                newName: "ReceivePayLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "ProductSupplierLogs",
                schema: "sepdb",
                newName: "ProductSupplierLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "ProductPriceLogs",
                schema: "sepdb",
                newName: "ProductPriceLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "ProductLogs",
                schema: "sepdb",
                newName: "ProductLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "ProductInventorieLogs",
                schema: "sepdb",
                newName: "ProductInventorieLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "ProductDetailLogs",
                schema: "sepdb",
                newName: "ProductDetailLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "ProductBrandLogs",
                schema: "sepdb",
                newName: "ProductBrandLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "OrderServiceLogs",
                schema: "sepdb",
                newName: "OrderServiceLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "OrderPaymentLogs",
                schema: "sepdb",
                newName: "OrderPaymentLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "OrderLogs",
                schema: "sepdb",
                newName: "OrderLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "OrderDetailLogs",
                schema: "sepdb",
                newName: "OrderDetailLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "LadingLicenseDetailLogs",
                schema: "sepdb",
                newName: "LadingLicenseDetailLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "CustomerLogs",
                schema: "sepdb",
                newName: "CustomerLogs",
                newSchema: "seplog");

            migrationBuilder.RenameTable(
                name: "CargoAnnouncementLogs",
                schema: "sepdb",
                newName: "CargoAnnouncementLogs",
                newSchema: "seplog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sepdb");

            migrationBuilder.RenameTable(
                name: "TableRecordRemovalInfoLogs",
                schema: "seplog",
                newName: "TableRecordRemovalInfoLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "ReceivePaymentSourceLogs",
                schema: "seplog",
                newName: "ReceivePaymentSourceLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "ReceivePayLogs",
                schema: "seplog",
                newName: "ReceivePayLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "ProductSupplierLogs",
                schema: "seplog",
                newName: "ProductSupplierLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "ProductPriceLogs",
                schema: "seplog",
                newName: "ProductPriceLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "ProductLogs",
                schema: "seplog",
                newName: "ProductLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "ProductInventorieLogs",
                schema: "seplog",
                newName: "ProductInventorieLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "ProductDetailLogs",
                schema: "seplog",
                newName: "ProductDetailLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "ProductBrandLogs",
                schema: "seplog",
                newName: "ProductBrandLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "OrderServiceLogs",
                schema: "seplog",
                newName: "OrderServiceLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "OrderPaymentLogs",
                schema: "seplog",
                newName: "OrderPaymentLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "OrderLogs",
                schema: "seplog",
                newName: "OrderLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "OrderDetailLogs",
                schema: "seplog",
                newName: "OrderDetailLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "LadingLicenseDetailLogs",
                schema: "seplog",
                newName: "LadingLicenseDetailLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "CustomerLogs",
                schema: "seplog",
                newName: "CustomerLogs",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "CargoAnnouncementLogs",
                schema: "seplog",
                newName: "CargoAnnouncementLogs",
                newSchema: "sepdb");
        }
    }
}
