using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409110137pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_PrimaryOrders_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropTable(
                name: "PrimaryOrders",
                schema: "sepdb");

            migrationBuilder.RenameColumn(
                name: "PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment",
                newName: "DraftOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachment_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment",
                newName: "IX_Attachment_DraftOrderId");

            migrationBuilder.AddColumn<int>(
                name: "DraftOrderCode",
                schema: "sepdb",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DraftOrders",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "IX_DraftOrders_CreatedBy",
                schema: "sepdb",
                table: "DraftOrders",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_DraftOrders_DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "DraftOrderId",
                principalSchema: "sepdb",
                principalTable: "DraftOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_DraftOrders_DraftOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropTable(
                name: "DraftOrders",
                schema: "sepdb");

            migrationBuilder.DropColumn(
                name: "DraftOrderCode",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                newName: "PrimaryOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachment_DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                newName: "IX_Attachment_PrimaryOrderId");

            migrationBuilder.CreateTable(
                name: "PrimaryOrders",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Converted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryOrders_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryOrders_CreatedBy",
                schema: "sepdb",
                table: "PrimaryOrders",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_PrimaryOrders_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "PrimaryOrderId",
                principalSchema: "sepdb",
                principalTable: "PrimaryOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
