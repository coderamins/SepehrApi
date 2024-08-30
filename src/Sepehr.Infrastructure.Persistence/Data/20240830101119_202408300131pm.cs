using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408300131pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentPayments_LadingExitPermit_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropIndex(
                name: "IX_RentPayments_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.DropColumn(
                name: "LadingExitPermitId",
                schema: "sepdb",
                table: "RentPayments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "sepdb",
                table: "RentPayments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1010, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RentPaymentId",
                schema: "sepdb",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RentPaymentDetail",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentPaymentId = table.Column<int>(type: "int", nullable: false),
                    UnloadingPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LadingExitPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentPaymentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentPaymentDetail_LadingExitPermit_LadingExitPermitId",
                        column: x => x.LadingExitPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingExitPermit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentPaymentDetail_RentPayments_RentPaymentId",
                        column: x => x.RentPaymentId,
                        principalSchema: "sepdb",
                        principalTable: "RentPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentPaymentDetail_UnloadingPermits_UnloadingPermitId",
                        column: x => x.UnloadingPermitId,
                        principalSchema: "sepdb",
                        principalTable: "UnloadingPermits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_RentPaymentId",
                schema: "sepdb",
                table: "Attachment",
                column: "RentPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPaymentDetail_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                column: "LadingExitPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPaymentDetail_RentPaymentId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                column: "RentPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPaymentDetail_UnloadingPermitId",
                schema: "sepdb",
                table: "RentPaymentDetail",
                column: "UnloadingPermitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_RentPayments_RentPaymentId",
                schema: "sepdb",
                table: "Attachment",
                column: "RentPaymentId",
                principalSchema: "sepdb",
                principalTable: "RentPayments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_RentPayments_RentPaymentId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropTable(
                name: "RentPaymentDetail",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_RentPaymentId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "RentPaymentId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "sepdb",
                table: "RentPayments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1010, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "LadingExitPermitId",
                schema: "sepdb",
                table: "RentPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPayments",
                column: "LadingExitPermitId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentPayments_LadingExitPermit_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPayments",
                column: "LadingExitPermitId",
                principalSchema: "sepdb",
                principalTable: "LadingExitPermit",
                principalColumn: "Id");
        }
    }
}
