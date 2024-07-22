using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202407210824pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CargoAnnounceId",
                schema: "sepdb",
                table: "Attachment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_CargoAnnounceId",
                schema: "sepdb",
                table: "Attachment",
                column: "CargoAnnounceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_CargoAnnounces_CargoAnnounceId",
                schema: "sepdb",
                table: "Attachment",
                column: "CargoAnnounceId",
                principalSchema: "sepdb",
                principalTable: "CargoAnnounces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_CargoAnnounces_CargoAnnounceId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_CargoAnnounceId",
                schema: "sepdb",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "CargoAnnounceId",
                schema: "sepdb",
                table: "Attachment");
        }
    }
}
