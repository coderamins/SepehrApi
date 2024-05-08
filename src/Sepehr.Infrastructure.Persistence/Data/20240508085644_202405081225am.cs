using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202405081225am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_CreatedBy",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ConfirmedStatus",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DeliverDate",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "TransferedAmount",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "LadingLicenseId",
                schema: "sepdb",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LadingLicense",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoAnnounceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasExitPermit = table.Column<bool>(type: "bit", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FareAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ExitPermitDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductSubUnitId = table.Column<int>(type: "int", nullable: true),
                    LadingExitPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingLicense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LadingLicense_CargoAnnounces_CargoAnnounceId",
                        column: x => x.CargoAnnounceId,
                        principalSchema: "sepdb",
                        principalTable: "CargoAnnounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingLicense_LadingExitPermit_LadingExitPermitId",
                        column: x => x.LadingExitPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingExitPermit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LadingLicense_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LadingLicense_ProductUnits_ProductSubUnitId",
                        column: x => x.ProductSubUnitId,
                        principalSchema: "sepdb",
                        principalTable: "ProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LadingLicense_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LadingLicenseDetail",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LadingLicenseId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    LadingAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RealAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PackageCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingLicenseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LadingLicenseDetail_LadingLicense_LadingLicenseId",
                        column: x => x.LadingLicenseId,
                        principalSchema: "sepdb",
                        principalTable: "LadingLicense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingLicenseDetail_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalSchema: "sepdb",
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_LadingLicenseId",
                schema: "sepdb",
                table: "Attachment",
                column: "LadingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_CargoAnnounceId",
                schema: "sepdb",
                table: "LadingLicense",
                column: "CargoAnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_CreatedBy",
                schema: "sepdb",
                table: "LadingLicense",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_LadingExitPermitId",
                schema: "sepdb",
                table: "LadingLicense",
                column: "LadingExitPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_OrderId",
                schema: "sepdb",
                table: "LadingLicense",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_ProductSubUnitId",
                schema: "sepdb",
                table: "LadingLicense",
                column: "ProductSubUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicenseDetail_LadingLicenseId",
                schema: "sepdb",
                table: "LadingLicenseDetail",
                column: "LadingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicenseDetail_OrderDetailId",
                schema: "sepdb",
                table: "LadingLicenseDetail",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_LadingLicense_LadingLicenseId",
                schema: "sepdb",
                table: "Attachment",
                column: "LadingLicenseId",
                principalSchema: "sepdb",
                principalTable: "LadingLicense",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_LadingLicense_LadingLicenseId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropTable(
                name: "LadingLicenseDetail",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "LadingLicense",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_LadingLicenseId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "LadingLicenseId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmedStatus",
                schema: "sepdb",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "sepdb",
                table: "OrderDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "sepdb",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliverDate",
                schema: "sepdb",
                table: "OrderDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "sepdb",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "sepdb",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TransferedAmount",
                schema: "sepdb",
                table: "OrderDetails",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CreatedBy",
                schema: "sepdb",
                table: "OrderDetails",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "OrderDetails",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
