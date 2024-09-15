using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409141116pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DraftOrderId1",
                schema: "sepdb",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DraftOrderId1",
                schema: "sepdb",
                table: "Attachment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DraftOrders",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DraftOrderCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    Converted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftOrders_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DraftOrderId1",
                schema: "sepdb",
                table: "Orders",
                column: "DraftOrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_DraftOrderId1",
                schema: "sepdb",
                table: "Attachment",
                column: "DraftOrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_DraftOrders_CreatedBy",
                schema: "sepdb",
                table: "DraftOrders",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_DraftOrders_DraftOrderId1",
                schema: "sepdb",
                table: "Attachment",
                column: "DraftOrderId1",
                principalSchema: "sepdb",
                principalTable: "DraftOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DraftOrders_DraftOrderId1",
                schema: "sepdb",
                table: "Orders",
                column: "DraftOrderId1",
                principalSchema: "sepdb",
                principalTable: "DraftOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_DraftOrders_DraftOrderId1",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DraftOrders_DraftOrderId1",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DraftOrders",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DraftOrderId1",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_DraftOrderId1",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DraftOrderId1",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "DraftOrderId1",
                schema: "sepdb",
                table: "Attachment");
        }
    }
}
