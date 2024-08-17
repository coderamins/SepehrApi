using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810170934pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Banks_BankId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_PaymentRequestReasons_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_Banks_BankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_PaymentRequestReasons_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePays_ReceivePaymentTypes_ReceivePaymentTypeFromId",
                schema: "sepdb",
                table: "ReceivePays");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePays_ReceivePaymentTypes_ReceivePaymentTypeToId",
                schema: "sepdb",
                table: "ReceivePays");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_ReceivePaymentTypes_ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropTable(
                name: "ReceivePaymentTypes",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_BankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_BankId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "BankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentRequestReasonId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "BankId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.RenameColumn(
                name: "PersonnelPaymentRequestDescription",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                newName: "PaymentRequestReasonDesc");

            migrationBuilder.RenameColumn(
                name: "ApplicatorName",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                newName: "PaymentRequestDescription");

            migrationBuilder.RenameColumn(
                name: "PersonnelPaymentRequestDescription",
                schema: "sepdb",
                table: "PaymentRequests",
                newName: "PaymentRequestReasonDesc");

            migrationBuilder.RenameColumn(
                name: "ApplicatorName",
                schema: "sepdb",
                table: "PaymentRequests",
                newName: "PaymentRequestDescription");

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromCostId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentFromCustomerId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromIncomeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentOriginTypeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromCostId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentFromCustomerId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromIncomeId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentOriginTypeId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentOriginTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOriginTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromCashDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromCostId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromCostId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromCustomerId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromIncomeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromIncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromOrganizationBankId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromPettyCashId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromShareHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentOriginTypeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentOriginTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromCashDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentFromCostId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromCostId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentFromCustomerId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentFromIncomeId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromIncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromOrganizationBankId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromPettyCashId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromShareHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentOriginTypeId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentOriginTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_CashDesks_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromCashDeskId",
                principalSchema: "sepdb",
                principalTable: "CashDesks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Costs_PaymentFromCostId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromCostId",
                principalSchema: "sepdb",
                principalTable: "Costs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Customers_PaymentFromCustomerId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromCustomerId",
                principalSchema: "sepdb",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Incomes_PaymentFromIncomeId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromIncomeId",
                principalSchema: "sepdb",
                principalTable: "Incomes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_OrganizationBanks_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromOrganizationBankId",
                principalSchema: "sepdb",
                principalTable: "OrganizationBanks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_PaymentOriginTypes_PaymentOriginTypeId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentOriginTypeId",
                principalSchema: "sepdb",
                principalTable: "PaymentOriginTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_PettyCashs_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromPettyCashId",
                principalSchema: "sepdb",
                principalTable: "PettyCashs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_ShareHolders_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentFromShareHolderId",
                principalSchema: "sepdb",
                principalTable: "ShareHolders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_CashDesks_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromCashDeskId",
                principalSchema: "sepdb",
                principalTable: "CashDesks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_Costs_PaymentFromCostId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromCostId",
                principalSchema: "sepdb",
                principalTable: "Costs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_Customers_PaymentFromCustomerId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromCustomerId",
                principalSchema: "sepdb",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_Incomes_PaymentFromIncomeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromIncomeId",
                principalSchema: "sepdb",
                principalTable: "Incomes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_OrganizationBanks_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromOrganizationBankId",
                principalSchema: "sepdb",
                principalTable: "OrganizationBanks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_PaymentOriginTypes_PaymentOriginTypeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentOriginTypeId",
                principalSchema: "sepdb",
                principalTable: "PaymentOriginTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_PettyCashs_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromPettyCashId",
                principalSchema: "sepdb",
                principalTable: "PettyCashs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_ShareHolders_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentFromShareHolderId",
                principalSchema: "sepdb",
                principalTable: "ShareHolders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePays_PaymentOriginTypes_ReceivePaymentTypeFromId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceivePaymentTypeFromId",
                principalSchema: "sepdb",
                principalTable: "PaymentOriginTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePays_PaymentOriginTypes_ReceivePaymentTypeToId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceivePaymentTypeToId",
                principalSchema: "sepdb",
                principalTable: "PaymentOriginTypes",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_CashDesks_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Costs_PaymentFromCostId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Customers_PaymentFromCustomerId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_Incomes_PaymentFromIncomeId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_OrganizationBanks_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_PaymentOriginTypes_PaymentOriginTypeId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_PettyCashs_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRequests_ShareHolders_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_CashDesks_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_Costs_PaymentFromCostId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_Customers_PaymentFromCustomerId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_Incomes_PaymentFromIncomeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_OrganizationBanks_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_PaymentOriginTypes_PaymentOriginTypeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_PettyCashs_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonnelPaymentRequests_ShareHolders_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePays_PaymentOriginTypes_ReceivePaymentTypeFromId",
                schema: "sepdb",
                table: "ReceivePays");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivePays_PaymentOriginTypes_ReceivePaymentTypeToId",
                schema: "sepdb",
                table: "ReceivePays");

            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_PaymentOriginTypes_ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropTable(
                name: "PaymentOriginTypes",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromCostId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromCustomerId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromIncomeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PersonnelPaymentRequests_PaymentOriginTypeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentFromCostId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentFromCustomerId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentFromIncomeId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRequests_PaymentOriginTypeId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromCostId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromCustomerId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromIncomeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentOriginTypeId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromCashDeskId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromCostId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromCustomerId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromIncomeId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromOrganizationBankId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromPettyCashId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentFromShareHolderId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PaymentOriginTypeId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.RenameColumn(
                name: "PaymentRequestReasonDesc",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                newName: "PersonnelPaymentRequestDescription");

            migrationBuilder.RenameColumn(
                name: "PaymentRequestDescription",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                newName: "ApplicatorName");

            migrationBuilder.RenameColumn(
                name: "PaymentRequestReasonDesc",
                schema: "sepdb",
                table: "PaymentRequests",
                newName: "PersonnelPaymentRequestDescription");

            migrationBuilder.RenameColumn(
                name: "PaymentRequestDescription",
                schema: "sepdb",
                table: "PaymentRequests",
                newName: "ApplicatorName");

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentRequestReasonId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReceivePaymentTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivePaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_BankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentRequestReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_BankId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentRequestReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_Banks_BankId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "BankId",
                principalSchema: "sepdb",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRequests_PaymentRequestReasons_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentRequestReasonId",
                principalSchema: "sepdb",
                principalTable: "PaymentRequestReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_Banks_BankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "BankId",
                principalSchema: "sepdb",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonnelPaymentRequests_PaymentRequestReasons_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentRequestReasonId",
                principalSchema: "sepdb",
                principalTable: "PaymentRequestReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePays_ReceivePaymentTypes_ReceivePaymentTypeFromId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceivePaymentTypeFromId",
                principalSchema: "sepdb",
                principalTable: "ReceivePaymentTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivePays_ReceivePaymentTypes_ReceivePaymentTypeToId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceivePaymentTypeToId",
                principalSchema: "sepdb",
                principalTable: "ReceivePaymentTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_ReceivePaymentTypes_ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments",
                column: "ReceivePaymentOriginId",
                principalSchema: "sepdb",
                principalTable: "ReceivePaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
