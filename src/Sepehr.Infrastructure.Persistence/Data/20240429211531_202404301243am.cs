using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202404301243am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AddColumn<bool>(
            //     name: "FareAmountApproved",
            //     schema: "sepdb",
            //     table: "TransferRemittances",
            //     type: "bit",
            //     nullable: false,
            //     defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FareAmountApproved",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceUnloadingPermits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            // migrationBuilder.AddColumn<bool>(
            //     name: "FareAmountApproved",
            //     schema: "sepdb",
            //     table: "LadingExitPermit",
            //     type: "bit",
            //     nullable: false,
            //     defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentType",
                schema: "sepdb",
                table: "Attachment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            // migrationBuilder.CreateTable(
            //     name: "DriverFareAmountApproves",
            //     schema: "sepdb",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //         PurOrderTransRemittUnloadingPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //         LadingExitPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //         CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //         Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //         LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
            //         IsActive = table.Column<bool>(type: "bit", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_DriverFareAmountApproves", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_DriverFareAmountApproves_LadingExitPermit_LadingExitPermitId",
            //             column: x => x.LadingExitPermitId,
            //             principalSchema: "sepdb",
            //             principalTable: "LadingExitPermit",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_DriverFareAmountApproves_PurchaseOrderTransferRemittanceUnloadingPermits_PurOrderTransRemittUnloadingPermitId",
            //             column: x => x.PurOrderTransRemittUnloadingPermitId,
            //             principalSchema: "sepdb",
            //             principalTable: "PurchaseOrderTransferRemittanceUnloadingPermits",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_DriverFareAmountApproves_Users_CreatedBy",
            //             column: x => x.CreatedBy,
            //             principalSchema: "sepdb",
            //             principalTable: "Users",
            //             principalColumn: "Id");
            //     });

            // migrationBuilder.CreateIndex(
            //     name: "IX_DriverFareAmountApproves_CreatedBy",
            //     schema: "sepdb",
            //     table: "DriverFareAmountApproves",
            //     column: "CreatedBy");

            // migrationBuilder.CreateIndex(
            //     name: "IX_DriverFareAmountApproves_LadingExitPermitId",
            //     schema: "sepdb",
            //     table: "DriverFareAmountApproves",
            //     column: "LadingExitPermitId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_DriverFareAmountApproves_PurOrderTransRemittUnloadingPermitId",
            //     schema: "sepdb",
            //     table: "DriverFareAmountApproves",
            //     column: "PurOrderTransRemittUnloadingPermitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverFareAmountApproves",
                schema: "sepdb");

            migrationBuilder.DropColumn(
                name: "FareAmountApproved",
                schema: "sepdb",
                table: "TransferRemittances");

            migrationBuilder.DropColumn(
                name: "FareAmountApproved",
                schema: "sepdb",
                table: "PurchaseOrderTransferRemittanceUnloadingPermits");

            migrationBuilder.DropColumn(
                name: "FareAmountApproved",
                schema: "sepdb",
                table: "LadingExitPermit");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentType",
                schema: "sepdb",
                table: "Attachment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
