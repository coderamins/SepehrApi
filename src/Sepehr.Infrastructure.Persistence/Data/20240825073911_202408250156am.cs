using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408250156am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_PurchaseInvoiceTypes_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_PurchaseInvoiceTypes_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_PaymentOriginTypes_ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRemittances_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances");

            migrationBuilder.DropTable(
                name: "PurchaseInvoiceTypes",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "FareAmountApproved",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropColumn(
                name: "FareAmountPayStatus",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropColumn(
                name: "FareAmountApproved",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.DropColumn(
                name: "FareAmountPayStatus",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.RenameColumn(
                name: "ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments",
                newName: "FareAmountStatusId");

            migrationBuilder.AddColumn<int>(
                name: "FareAmountStatusId",
                schema: "sepdb",
                table: "UnloadingPermits",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromCashDeskId",
                schema: "sepdb",
                table: "RentPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromCostId",
                schema: "sepdb",
                table: "RentPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentFromCustomerId",
                schema: "sepdb",
                table: "RentPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromIncomeId",
                schema: "sepdb",
                table: "RentPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "RentPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromPettyCashId",
                schema: "sepdb",
                table: "RentPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentFromShareHolderId",
                schema: "sepdb",
                table: "RentPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentOriginTypeId",
                schema: "sepdb",
                table: "RentPayments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FareAmountStatusId",
                schema: "sepdb",
                table: "LadingExitPermit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FareAmountStatus",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FareAmountStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermits_FareAmountStatusId",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "FareAmountStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromCashDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_PaymentFromCostId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromCostId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_PaymentFromCustomerId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_PaymentFromIncomeId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromIncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromOrganizationBankId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromPettyCashId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromShareHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_PaymentOriginTypeId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentOriginTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermit_FareAmountStatusId",
                schema: "sepdb",
                table: "LadingExitPermit",
                column: "FareAmountStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LadingExitPermit_FareAmountStatus_FareAmountStatusId",
                schema: "sepdb",
                table: "LadingExitPermit",
                column: "FareAmountStatusId",
                principalSchema: "sepdb",
                principalTable: "FareAmountStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_InvoiceTypes_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "PurchaseInvoiceTypeId",
                principalSchema: "sepdb",
                principalTable: "InvoiceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_InvoiceTypes_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "PurchaseInvoiceTypeId",
                principalSchema: "sepdb",
                principalTable: "InvoiceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_CashDesks_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromCashDeskId",
                principalSchema: "sepdb",
                principalTable: "CashDesks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_Costs_PaymentFromCostId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromCostId",
                principalSchema: "sepdb",
                principalTable: "Costs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_Customers_PaymentFromCustomerId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromCustomerId",
                principalSchema: "sepdb",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_Incomes_PaymentFromIncomeId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromIncomeId",
                principalSchema: "sepdb",
                principalTable: "Incomes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_OrganizationBanks_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromOrganizationBankId",
                principalSchema: "sepdb",
                principalTable: "OrganizationBanks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_PaymentOriginTypes_PaymentOriginTypeId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentOriginTypeId",
                principalSchema: "sepdb",
                principalTable: "PaymentOriginTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_PettyCashs_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromPettyCashId",
                principalSchema: "sepdb",
                principalTable: "PettyCashs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_ShareHolders_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "RentPayments",
                column: "PaymentFromShareHolderId",
                principalSchema: "sepdb",
                principalTable: "ShareHolders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRemittances_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "PurchaseOrderId",
                principalSchema: "sepdb",
                principalTable: "PurchaseOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnloadingPermits_FareAmountStatus_FareAmountStatusId",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "FareAmountStatusId",
                principalSchema: "sepdb",
                principalTable: "FareAmountStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LadingExitPermit_FareAmountStatus_FareAmountStatusId",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_InvoiceTypes_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_InvoiceTypes_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_CashDesks_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_Costs_PaymentFromCostId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_Customers_PaymentFromCustomerId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_Incomes_PaymentFromIncomeId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_OrganizationBanks_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_PaymentOriginTypes_PaymentOriginTypeId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_PettyCashs_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_ShareHolders_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferRemittances_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances");

            migrationBuilder.DropForeignKey(
                name: "FK_UnloadingPermits_FareAmountStatus_FareAmountStatusId",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropTable(
                name: "FareAmountStatus",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_UnloadingPermits_FareAmountStatusId",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_PaymentFromCostId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_PaymentFromCustomerId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_PaymentFromIncomeId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_PaymentOriginTypeId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_LadingExitPermit_FareAmountStatusId",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.DropColumn(
                name: "FareAmountStatusId",
                schema: "sepdb",
                table: "UnloadingPermits");

            migrationBuilder.DropColumn(
                name: "PaymentFromCashDeskId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "PaymentFromCostId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "PaymentFromCustomerId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "PaymentFromIncomeId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "PaymentFromPettyCashId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "PaymentFromShareHolderId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "PaymentOriginTypeId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "FareAmountStatusId",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.RenameColumn(
                name: "FareAmountStatusId",
                schema: "sepdb",
                table: "RentPayments",
                newName: "ReceivePaymentOriginId");

            migrationBuilder.AddColumn<bool>(
                name: "FareAmountApproved",
                schema: "sepdb",
                table: "UnloadingPermits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FareAmountPayStatus",
                schema: "sepdb",
                table: "UnloadingPermits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "FareAmountApproved",
                schema: "sepdb",
                table: "LadingExitPermit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FareAmountPayStatus",
                schema: "sepdb",
                table: "LadingExitPermit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PurchaseInvoiceTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments",
                column: "ReceivePaymentOriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_PurchaseInvoiceTypes_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "PurchaseInvoiceTypeId",
                principalSchema: "sepdb",
                principalTable: "PurchaseInvoiceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_PurchaseInvoiceTypes_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "PurchaseInvoiceTypeId",
                principalSchema: "sepdb",
                principalTable: "PurchaseInvoiceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_PaymentOriginTypes_ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments",
                column: "ReceivePaymentOriginId",
                principalSchema: "sepdb",
                principalTable: "PaymentOriginTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferRemittances_PurchaseOrder_PurchaseOrderId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "PurchaseOrderId",
                principalSchema: "sepdb",
                principalTable: "PurchaseOrder",
                principalColumn: "Id");
        }
    }
}
