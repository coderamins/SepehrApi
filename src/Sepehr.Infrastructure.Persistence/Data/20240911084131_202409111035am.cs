using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409111035am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                schema: "sepdb",
                table: "PrimaryOrders");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "PrimaryOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_PrimaryOrders_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "PrimaryOrderId",
                principalSchema: "sepdb",
                principalTable: "PrimaryOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_PrimaryOrders_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "PrimaryOrderId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                schema: "sepdb",
                table: "PrimaryOrders",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
