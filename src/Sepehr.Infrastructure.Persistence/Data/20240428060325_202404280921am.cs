using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202404280921am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittances_CreatedBy",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShareHolders_CreatedBy",
                schema: "sepdb",
                table: "ShareHolders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_CreatedBy",
                schema: "sepdb",
                table: "RentPayments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_CreatedBy",
                schema: "sepdb",
                table: "ReceivePays",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTransfers_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransfers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTransferRemittanceUnloadingPermits_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceUnloadingPermits",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTransferRemittanceEntrancePermits_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceEntrancePermits",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_CreatedBy",
                schema: "sepdb",
                table: "ProductSuppliers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedBy",
                schema: "sepdb",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_CreatedBy",
                schema: "sepdb",
                table: "ProductPrices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryHistories_CreatedBy",
                schema: "sepdb",
                table: "ProductInventoryHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_CreatedBy",
                schema: "sepdb",
                table: "ProductInventories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedBy",
                schema: "sepdb",
                table: "Orders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialWarehoseInventories_CreatedBy",
                schema: "sepdb",
                table: "OfficialWarehoseInventories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LadingPermit_CreatedBy",
                schema: "sepdb",
                table: "LadingPermit",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermitDetails_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermitDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermit_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermit",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedBy",
                schema: "sepdb",
                table: "Customers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOfficialCompanies_CreatedBy",
                schema: "sepdb",
                table: "CustomerOfficialCompanies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnounceDetails_CreatedBy",
                schema: "sepdb",
                table: "CargoAnnounceDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationMenus_CreatedBy",
                schema: "sepdb",
                table: "ApplicationMenus",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationMenus_Users_CreatedBy",
                schema: "sepdb",
                table: "ApplicationMenus",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoAnnounceDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "CargoAnnounceDetails",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOfficialCompanies_Users_CreatedBy",
                schema: "sepdb",
                table: "CustomerOfficialCompanies",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_CreatedBy",
                schema: "sepdb",
                table: "Customers",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LadingExitPermit_Users_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermit",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LadingExitPermitDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermitDetails",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LadingPermit_Users_CreatedBy",
                schema: "sepdb",
                table: "LadingPermit",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialWarehoseInventories_Users_CreatedBy",
                schema: "sepdb",
                table: "OfficialWarehoseInventories",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CreatedBy",
                schema: "sepdb",
                table: "Orders",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventories_Users_CreatedBy",
                schema: "sepdb",
                table: "ProductInventories",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryHistories_Users_CreatedBy",
                schema: "sepdb",
                table: "ProductInventoryHistories",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_Users_CreatedBy",
                schema: "sepdb",
                table: "ProductPrices",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedBy",
                schema: "sepdb",
                table: "Products",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSuppliers_Users_CreatedBy",
                schema: "sepdb",
                table: "ProductSuppliers",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderTransferRemittanceEntrancePermits_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceEntrancePermits",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderTransferRemittanceUnloadingPermits_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceUnloadingPermits",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderTransfers_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransfers",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePays_Users_CreatedBy",
                schema: "sepdb",
                table: "ReceivePays",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_Users_CreatedBy",
                schema: "sepdb",
                table: "RentPayments",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareHolders_Users_CreatedBy",
                schema: "sepdb",
                table: "ShareHolders",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRemittances_Users_CreatedBy",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationMenus_Users_CreatedBy",
                schema: "sepdb",
                table: "ApplicationMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoAnnounceDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "CargoAnnounceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOfficialCompanies_Users_CreatedBy",
                schema: "sepdb",
                table: "CustomerOfficialCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_CreatedBy",
                schema: "sepdb",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_LadingExitPermit_Users_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.DropForeignKey(
                name: "FK_LadingExitPermitDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermitDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_LadingPermit_Users_CreatedBy",
                schema: "sepdb",
                table: "LadingPermit");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialWarehoseInventories_Users_CreatedBy",
                schema: "sepdb",
                table: "OfficialWarehoseInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CreatedBy",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventories_Users_CreatedBy",
                schema: "sepdb",
                table: "ProductInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryHistories_Users_CreatedBy",
                schema: "sepdb",
                table: "ProductInventoryHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_Users_CreatedBy",
                schema: "sepdb",
                table: "ProductPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedBy",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSuppliers_Users_CreatedBy",
                schema: "sepdb",
                table: "ProductSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderTransferRemittanceEntrancePermits_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceEntrancePermits");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderTransferRemittanceUnloadingPermits_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceUnloadingPermits");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderTransfers_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePays_Users_CreatedBy",
                schema: "sepdb",
                table: "ReceivePays");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_Users_CreatedBy",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ShareHolders_Users_CreatedBy",
                schema: "sepdb",
                table: "ShareHolders");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRemittances_Users_CreatedBy",
                schema: "sepdb",
                table: "TransferRemittances");

            migrationBuilder.DropIndex(
                name: "IX_TransferRemittances_CreatedBy",
                schema: "sepdb",
                table: "TransferRemittances");

            migrationBuilder.DropIndex(
                name: "IX_ShareHolders_CreatedBy",
                schema: "sepdb",
                table: "ShareHolders");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_CreatedBy",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_ReceivePays_CreatedBy",
                schema: "sepdb",
                table: "ReceivePays");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderTransfers_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransfers");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderTransferRemittanceUnloadingPermits_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceUnloadingPermits");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderTransferRemittanceEntrancePermits_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceEntrancePermits");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrder_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductSuppliers_CreatedBy",
                schema: "sepdb",
                table: "ProductSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedBy",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_CreatedBy",
                schema: "sepdb",
                table: "ProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventoryHistories_CreatedBy",
                schema: "sepdb",
                table: "ProductInventoryHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventories_CreatedBy",
                schema: "sepdb",
                table: "ProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatedBy",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OfficialWarehoseInventories_CreatedBy",
                schema: "sepdb",
                table: "OfficialWarehoseInventories");

            migrationBuilder.DropIndex(
                name: "IX_LadingPermit_CreatedBy",
                schema: "sepdb",
                table: "LadingPermit");

            migrationBuilder.DropIndex(
                name: "IX_LadingExitPermitDetails_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermitDetails");

            migrationBuilder.DropIndex(
                name: "IX_LadingExitPermit_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreatedBy",
                schema: "sepdb",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOfficialCompanies_CreatedBy",
                schema: "sepdb",
                table: "CustomerOfficialCompanies");

            migrationBuilder.DropIndex(
                name: "IX_CargoAnnounceDetails_CreatedBy",
                schema: "sepdb",
                table: "CargoAnnounceDetails");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationMenus_CreatedBy",
                schema: "sepdb",
                table: "ApplicationMenus");
        }
    }
}
