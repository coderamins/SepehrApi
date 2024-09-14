using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409111100am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_PrimaryOrders_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_PrimaryOrders_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_PrimaryOrders_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "PrimaryOrderId",
                principalSchema: "sepdb",
                principalTable: "PrimaryOrders",
                principalColumn: "Id");
        }
    }
}
