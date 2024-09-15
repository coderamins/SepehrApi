using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409141053pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_DraftOrder_DraftOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DraftOrder_DraftOrderId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DraftOrder",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DraftOrderId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_DraftOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Attachment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DraftOrder",
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
                    table.PrimaryKey("PK_DraftOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraftOrder_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                column: "DraftOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "DraftOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DraftOrder_CreatedBy",
                schema: "sepdb",
                table: "DraftOrder",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_DraftOrder_DraftOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "DraftOrderId",
                principalSchema: "sepdb",
                principalTable: "DraftOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DraftOrder_DraftOrderId",
                schema: "sepdb",
                table: "Orders",
                column: "DraftOrderId",
                principalSchema: "sepdb",
                principalTable: "DraftOrder",
                principalColumn: "Id");
        }
    }
}
