using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408120505pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentRequestId",
                schema: "sepdb",
                table: "Attachment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_PaymentRequestId",
                schema: "sepdb",
                table: "Attachment",
                column: "PaymentRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_PaymentRequests_PaymentRequestId",
                schema: "sepdb",
                table: "Attachment",
                column: "PaymentRequestId",
                principalSchema: "sepdb",
                principalTable: "PaymentRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_PaymentRequests_PaymentRequestId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_PaymentRequestId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "PaymentRequestId",
                schema: "sepdb",
                table: "Attachment");
        }
    }
}
