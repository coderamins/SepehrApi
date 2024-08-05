using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408060120am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabels_CustomerLabelType_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLabels_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerLabelType",
                schema: "sepdb",
                table: "CustomerLabelType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "sepdb",
                table: "CustomerLabelType");

            migrationBuilder.RenameTable(
                name: "CustomerLabelType",
                schema: "sepdb",
                newName: "CustomerLabelTypes",
                newSchema: "sepdb");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "sepdb",
                table: "CustomerLabelTypes",
                newName: "CustomerLabelTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerLabelTypes",
                schema: "sepdb",
                table: "CustomerLabelTypes",
                column: "CustomerLabelTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerLabelTypes",
                schema: "sepdb",
                table: "CustomerLabelTypes");

            migrationBuilder.RenameTable(
                name: "CustomerLabelTypes",
                schema: "sepdb",
                newName: "CustomerLabelType",
                newSchema: "sepdb");

            migrationBuilder.RenameColumn(
                name: "CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabelType",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "sepdb",
                table: "CustomerLabelType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerLabelType",
                schema: "sepdb",
                table: "CustomerLabelType",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabels_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "CustomerLabelTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabels_CustomerLabelType_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "CustomerLabelTypeId",
                principalSchema: "sepdb",
                principalTable: "CustomerLabelType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
