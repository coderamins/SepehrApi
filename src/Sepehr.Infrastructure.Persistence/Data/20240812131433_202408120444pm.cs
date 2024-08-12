using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408120444pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonnelId",
                schema: "sepdb",
                table: "Phonebook",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentRequestReasons",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ReasonDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequestReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRequestStatus",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequestStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personnels",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonnelCode = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnels_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PaymentReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountOrShabaNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    ApplicatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentRequestStatusId = table.Column<int>(type: "int", nullable: false),
                    PaymentRequestDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectReasonDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRequests_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "sepdb",
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentRequests_PaymentRequestStatus_PaymentRequestStatusId",
                        column: x => x.PaymentRequestStatusId,
                        principalSchema: "sepdb",
                        principalTable: "PaymentRequestStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentRequests_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentRequests_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_PersonnelId",
                schema: "sepdb",
                table: "Phonebook",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_ApproverId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_BankId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_CreatedBy",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_PaymentRequestStatusId",
                schema: "sepdb",
                table: "PaymentRequests",
                column: "PaymentRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_CreatedBy",
                schema: "sepdb",
                table: "Personnels",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Phonebook_Personnels_PersonnelId",
                schema: "sepdb",
                table: "Phonebook",
                column: "PersonnelId",
                principalSchema: "sepdb",
                principalTable: "Personnels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phonebook_Personnels_PersonnelId",
                schema: "sepdb",
                table: "Phonebook");

            migrationBuilder.DropTable(
                name: "PaymentRequestReasons",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PaymentRequests",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Personnels",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PaymentRequestStatus",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_Phonebook_PersonnelId",
                schema: "sepdb",
                table: "Phonebook");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                schema: "sepdb",
                table: "Phonebook");
        }
    }
}
