using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202405030145am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "IX_PurchaseOrderDetails_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "CreatedBy");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_Users_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderDetails_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_CreatedBy",
                schema: "sepdb",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "LastModified",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails");

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
        }
    }
}
