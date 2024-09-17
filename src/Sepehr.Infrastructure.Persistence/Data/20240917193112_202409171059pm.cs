using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409171059pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SalesAgentId",
                schema: "sepdb",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SalesAgentId",
                schema: "sepdb",
                table: "Orders",
                column: "SalesAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_SalesAgentId",
                schema: "sepdb",
                table: "Orders",
                column: "SalesAgentId",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_SalesAgentId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SalesAgentId",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SalesAgentId",
                schema: "sepdb",
                table: "Orders");
        }
    }
}
