using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _20240810140812pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentRequestDescription",
                schema: "sepdb",
                table: "PaymentRequests",
                newName: "PersonnelPaymentRequestDescription");

            migrationBuilder.AddColumn<int>(
                name: "PaymentRequestTypeId",
                schema: "sepdb",
                table: "PaymentRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonnelPaymentRequestId",
                schema: "sepdb",
                table: "Attachment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonnelPaymentRequests",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonnelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PaymentRequestCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1000, 1"),
                    PaymentRequestTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PaymentRequestReasonId = table.Column<int>(type: "int", nullable: false),
                    BankAccountOrShabaNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    ApplicatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentRequestStatusId = table.Column<int>(type: "int", nullable: false),
                    PersonnelPaymentRequestDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectReasonDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonnelPaymentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonnelPaymentRequests_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "sepdb",
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelPaymentRequests_PaymentRequestReasons_PaymentRequestReasonId",
                        column: x => x.PaymentRequestReasonId,
                        principalSchema: "sepdb",
                        principalTable: "PaymentRequestReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelPaymentRequests_PaymentRequestStatus_PaymentRequestStatusId",
                        column: x => x.PaymentRequestStatusId,
                        principalSchema: "sepdb",
                        principalTable: "PaymentRequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelPaymentRequests_Personnels_PersonnelId",
                        column: x => x.PersonnelId,
                        principalSchema: "sepdb",
                        principalTable: "Personnels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonnelPaymentRequests_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonnelPaymentRequests_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_PersonnelPaymentRequestId",
                schema: "sepdb",
                table: "Attachment",
                column: "PersonnelPaymentRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_ApproverId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_BankId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_CreatedBy",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentRequestReasonId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentRequestReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PaymentRequestStatusId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PaymentRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonnelPaymentRequests_PersonnelId",
                schema: "sepdb",
                table: "PersonnelPaymentRequests",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_PersonnelPaymentRequests_PersonnelPaymentRequestId",
                schema: "sepdb",
                table: "Attachment",
                column: "PersonnelPaymentRequestId",
                principalSchema: "sepdb",
                principalTable: "PersonnelPaymentRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_PersonnelPaymentRequests_PersonnelPaymentRequestId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropTable(
                name: "PersonnelPaymentRequests",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_PersonnelPaymentRequestId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "PaymentRequestTypeId",
                schema: "sepdb",
                table: "PaymentRequests");

            migrationBuilder.DropColumn(
                name: "PersonnelPaymentRequestId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.RenameColumn(
                name: "PersonnelPaymentRequestDescription",
                schema: "sepdb",
                table: "PaymentRequests",
                newName: "PaymentRequestDescription");
        }
    }
}
